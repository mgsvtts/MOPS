using Domain.TypeAggregate.Repositories;
using Mediator;

namespace Application.Queries.Types.GetAll;

public sealed class GetAllTypesQueryHandler : IQueryHandler<GetAllTypesQuery, IEnumerable<Domain.TypeAggregate.Type>>
{
    private readonly ITypeRepository _repository;

    public GetAllTypesQueryHandler(ITypeRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<IEnumerable<Domain.TypeAggregate.Type>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}