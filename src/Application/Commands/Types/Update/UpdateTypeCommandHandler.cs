using Domain.TypeAggregate.Repositories;
using Mapster;
using Mediator;

namespace Application.Commands.Types.Update;

public sealed class UpdateTypeCommandHandler : ICommandHandler<UpdateTypeCommand, Domain.TypeAggregate.Type>
{
    private readonly ITypeRepository _typeRepository;

    public UpdateTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async ValueTask<Domain.TypeAggregate.Type> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = request.Adapt<Domain.TypeAggregate.Type>();

        await _typeRepository.UpdateAsync(type, cancellationToken);

        return type;
    }
}