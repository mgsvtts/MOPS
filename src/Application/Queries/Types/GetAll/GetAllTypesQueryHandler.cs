using Domain.TypeAggregate.Repositories;
using Mediator;

namespace Application.Queries.Types.GetAll;

public sealed class GetAllTypesQueryHandler(ITypeRepository _repository) : IQueryHandler<GetAllTypesQuery, IEnumerable<Domain.TypeAggregate.Type>>
{
    public async ValueTask<IEnumerable<Domain.TypeAggregate.Type>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}