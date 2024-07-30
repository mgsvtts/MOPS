using Mediator;

namespace Application.Queries.Types.GetAll;

public sealed record GetAllTypesQuery() : IQuery<IEnumerable<Domain.TypeAggregate.Type>>;