using Application.Queries.Common;
using Mediator;

namespace Application.Queries.Types.GetAll;

public sealed record GetAllTypesQuery(PaginationMeta Meta) : IQuery<Pagination<Infrastructure.Models.Type>>;