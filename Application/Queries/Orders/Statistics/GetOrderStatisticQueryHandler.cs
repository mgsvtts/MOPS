using Application.Queries.Orders.Statistics.Dto;
using Dapper;
using Infrastructure;
using Infrastructure.Models;
using MapsterMapper;
using MediatR;

namespace Application.Queries.Orders.Statistics;

public sealed class GetOrderStatisticQueryHandler : IRequestHandler<GetOrderStatisticQuery, GetOrderStatisticQueryResponse>
{
    private readonly DbContext _db;
    private readonly IMapper _mapper;

    public GetOrderStatisticQueryHandler(DbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GetOrderStatisticQueryResponse> Handle(GetOrderStatisticQuery request, CancellationToken cancellationToken)
    {
        var statisticQuery = @$"SELECT items.{nameof(merch_items.name)},
                                      COUNT(o.{nameof(order_items.merch_item_id)}) AS orders_with_item,
	                                  SUM(o.{nameof(order_items.self_price)}) AS total_self_price,
                                      SUM(o.{nameof(order_items.price)}) AS total_price,
                                      SUM(o.{nameof(order_items.amount)}) AS total_amount
                               FROM {nameof(merch_items)} AS items
                               
                               LEFT JOIN {nameof(order_items)} AS o ON o.{nameof(order_items.merch_item_id)} = items.id
                               LEFT JOIN {nameof(orders)} AS ord ON ord.{nameof(orders.id)} = o.{nameof(order_items.order_id)}
                                
                               WHERE ord.created_at BETWEEN '{request.DateFrom:yyyy-MM-dd}' AND '{request.DateTo:yyyy-MM-dd}' 
                
                               GROUP BY
                                   items.id

                               ORDER BY items.{nameof(merch_items.name)};

                               SELECT SUM(i.{nameof(order_items.price)}) as absolute_price,
                                      SUM(i.{nameof(order_items.self_price)}) as absolute_self_price,
                                      SUM(i.{nameof(order_items.amount)}) as absolute_amount_sold

                               FROM {nameof(order_items)} as i

                               LEFT JOIN {nameof(orders)} as o on o.{nameof(orders.id)}=i.{nameof(order_items.order_id)}

                               WHERE o.{nameof(orders.created_at)} BETWEEN '{request.DateFrom:yyyy-MM-dd}' AND '{request.DateTo:yyyy-MM-dd}';";

        using var connection = _db.CreateConnection();

        var results = await connection.QueryMultipleAsync(statisticQuery);

        var totals = await results.ReadAsync<Dto.MerchItemStatistic>();
        var absolutePrice = await results.ReadFirstAsync<MerchItemAbsoluteStatistic>();

        return new GetOrderStatisticQueryResponse(_mapper.Map<IEnumerable<MerchItemStatistic>>(totals),
                                                  absolutePrice.absolute_amount_sold,
                                                  absolutePrice.absolute_price,
                                                  absolutePrice.absolute_self_price);
    }
}
