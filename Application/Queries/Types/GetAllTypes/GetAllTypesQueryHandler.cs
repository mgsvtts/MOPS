using Domain.TypeAggregate.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Queries.Types.GetAllTypes;

internal sealed class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IEnumerable<Domain.TypeAggregate.Type>>
{
    private readonly ITypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTypesQueryHandler(IMapper mapper, ITypeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<Domain.TypeAggregate.Type>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
