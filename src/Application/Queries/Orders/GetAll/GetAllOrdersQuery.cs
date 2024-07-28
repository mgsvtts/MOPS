using Mediator;

namespace Application.Queries.Orders.GetAll;
public sealed record GetAllOrdersQuery() : IQuery<IEnumerable<GetAllOrdersResponseOrder>>;
