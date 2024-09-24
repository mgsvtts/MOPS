using Domain.TypeAggregate.Repositories;
using Mapster;
using Mediator;

namespace Application.Commands.Types.Create;

public sealed class CreateTypeCommandHandler(ITypeRepository _typeRepository) : ICommandHandler<CreateTypeCommand, Domain.TypeAggregate.Type>
{
    public async ValueTask<Domain.TypeAggregate.Type> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
    {
        var type = request.Adapt<Domain.TypeAggregate.Type>();

        await _typeRepository.AddAsync(type, cancellationToken);

        return type;
    }
}