using MediatR;

namespace Application.Queries.Types.GetAll;

public record struct GetAllTypesQuery() : IRequest<IEnumerable<Domain.TypeAggregate.Type>>;
