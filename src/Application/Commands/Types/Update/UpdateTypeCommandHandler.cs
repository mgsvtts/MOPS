using Domain.TypeAggregate.Repositories;
using Mapster;
using Mediator;

namespace Application.Commands.Types.Update;

public sealed class UpdateTypeCommandHandler(ITypeRepository _typeRepository) : ICommandHandler<UpdateTypeCommand, Domain.TypeAggregate.Type>
{
    public async ValueTask<Domain.TypeAggregate.Type> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Type with id: {request.Id.Identity} not found");

        type = request.Adapt<Domain.TypeAggregate.Type>();

        await _typeRepository.UpdateAsync(type, cancellationToken);

        return type;
    }
}