using MediatR;

namespace Application.Queries.Orders.Statistics;
public readonly record struct GetOrderStatisticQuery : IRequest<GetOrderStatisticQueryResponse>
{
    public DateTime DateFrom { get; init; }
    public DateTime DateTo { get; init; }

    public GetOrderStatisticQuery(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        DateFrom = dateFrom ?? DateTime.MinValue;
        DateTo = dateTo ?? DateTime.MaxValue;
    }
}
