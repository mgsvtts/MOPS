using Domain.TypeAggregate.Repositories;
using Mapster;
using Mediator;

namespace Application.Commands.Types.Create;

public sealed class CreateTypeCommandHandler : ICommandHandler<CreateTypeCommand, Domain.TypeAggregate.Type>
{
    private readonly ITypeRepository _typeRepository;

    public CreateTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async ValueTask<Domain.TypeAggregate.Type> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = request.Adapt<Domain.TypeAggregate.Type>();

        await _typeRepository.AddAsync(type, cancellationToken);

        return type;
    }
}