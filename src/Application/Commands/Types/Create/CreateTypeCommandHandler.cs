using Domain.TypeAggregate.Repositories;
using MapsterMapper;
using Mediator;

namespace Application.Commands.Types.Create;

public sealed class CreateTypeCommandHandler : ICommandHandler<CreateTypeCommand, Domain.TypeAggregate.Type>
{
    private readonly IMapper _mapper;
    private readonly ITypeRepository _typeRepository;

    public CreateTypeCommandHandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    public async ValueTask<Domain.TypeAggregate.Type> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = _mapper.Map<Domain.TypeAggregate.Type>(request);

        await _typeRepository.AddAsync(type);

        return type;
    }
}
