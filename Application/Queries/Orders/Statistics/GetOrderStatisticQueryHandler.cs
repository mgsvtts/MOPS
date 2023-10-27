using Application.Queries.Orders.Statistics.Dto.Absolutes;
using Dapper;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure;
using Infrastructure.Models;
using MapsterMapper;
using MediatR;
using System.Diagnostics;
using System.Xml.Linq;

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
                               FROM
                                   {nameof(merch_items)} AS items
                               LEFT JOIN
                                   {nameof(order_items)} AS o
                               ON
                                   o.{nameof(order_items.merch_item_id)} = items.id
                               GROUP BY
                                   items.id
                               ORDER BY items.{nameof(merch_items.name)};

                               SELECT SUM({nameof(order_items.price)}) as absolute_price
                               FROM {nameof(order_items)};

                               SELECT SUM({nameof(order_items.self_price)}) as absolute_self_price
                               FROM {nameof(order_items)};

                               SELECT SUM({nameof(order_items.amount)}) as absolute_amount_sold
                               FROM {nameof(order_items)};

                               SELECT SUM({nameof(merch_items.amount)}) as absolute_amount_left
                               FROM {nameof(merch_items)};";

        using var connection = _db.CreateConnection();

        var results = await connection.QueryMultipleAsync(statisticQuery);

        var totals = await results.ReadAsync<Dto.MerchItemStatistic>();
        var absolutePrice = await results.ReadFirstAsync<MerchItemAbsolutePrice>();
        var absoluteSelfPrice = await results.ReadFirstAsync<MerchItemAbsoluteSelfPrice>();
        var absoluteAmountSold = await results.ReadFirstAsync<MerchItemAbsoluteAmountSold>();
        var absoluteAmountLeft = await results.ReadFirstAsync<MerchItemAbsoluteAmountLeft>();

        return new GetOrderStatisticQueryResponse(_mapper.Map<IEnumerable<MerchItemStatistic>>(totals),
                                                  absoluteAmountSold.absolute_amount_sold,
                                                  absoluteAmountLeft.absolute_amount_left,
                                                  absolutePrice.absolute_price,
                                                  absoluteSelfPrice.absolute_self_price);
    }
}
