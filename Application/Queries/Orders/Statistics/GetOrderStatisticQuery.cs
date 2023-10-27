using MediatR;

namespace Application.Queries.Orders.Statistics;
public record struct GetOrderStatisticQuery() : IRequest<GetOrderStatisticQueryResponse>;
