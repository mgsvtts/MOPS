using MediatR;

namespace Application.Queries.Orders.Statistics;
public record struct GetOrderStatisticQuery(DateTime DateFrom, DateTime DateTo) : IRequest<GetOrderStatisticQueryResponse>;
