using Application.Queries.Common;
using Mediator;

namespace Application.Queries.Orders.GetAll;
public sealed record GetAllOrdersQuery(PaginationMeta Meta) : IQuery<Pagination<GetAllOrdersResponseOrder>>;