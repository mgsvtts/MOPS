using Domain.TypeAggregate.Repositories;
using MapsterMapper;
using Mediator;

namespace Application.Commands.Types.Update;

public sealed class UpdateTypeCommandHandler : ICommandHandler<UpdateTypeCommand, Domain.TypeAggregate.Type>
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public UpdateTypeCommandHandler(ITypeRepository typeRepository, IMapper mapper)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    public async ValueTask<Domain.TypeAggregate.Type> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = _mapper.Map<Domain.TypeAggregate.Type>(request);

        await _typeRepository.UpdateAsync(type);

        return type;
    }
}
