using MediatR;

namespace Application.Queries.Orders.GetAllOrders;
public record struct GetAllOrdersQuery() : IRequest<IEnumerable<GetAllOrdersResponseOrder>>;
