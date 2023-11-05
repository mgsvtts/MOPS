using Application.Queries.Orders.Statistics.Dto;
using Dapper;
using Infrastructure;
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
        var statisticQuery = Infrastructure.Queries.Order.GetStatistic(request.DateFrom, request.DateTo);

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
