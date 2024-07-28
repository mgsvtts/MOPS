using Domain.TypeAggregate.Repositories;
using MapsterMapper;
using Mediator;

namespace Application.Queries.Types.GetAll;

internal sealed class GetAllTypesQueryHandler : IQueryHandler<GetAllTypesQuery, IEnumerable<Domain.TypeAggregate.Type>>
{
    private readonly ITypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTypesQueryHandler(IMapper mapper, ITypeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async ValueTask<IEnumerable<Domain.TypeAggregate.Type>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
