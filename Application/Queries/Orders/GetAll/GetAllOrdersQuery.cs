using MediatR;

namespace Application.Queries.Orders.GetAll;
public record struct GetAllOrdersQuery() : IRequest<IEnumerable<GetAllOrdersResponseOrder>>;
