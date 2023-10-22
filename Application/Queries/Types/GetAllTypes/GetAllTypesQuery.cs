using MediatR;

namespace Application.Queries.Types.GetAllTypes;

public sealed record GetAllTypesQuery() : IRequest<IEnumerable<Domain.TypeAggregate.Type>>;