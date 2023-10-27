using MediatR;

namespace Application.Queries.Types.GetAllTypes;

public record struct GetAllTypesQuery() : IRequest<IEnumerable<Domain.TypeAggregate.Type>>;
