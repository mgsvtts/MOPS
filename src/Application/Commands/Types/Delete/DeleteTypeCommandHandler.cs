using Domain.TypeAggregate.Repositories;
using Mediator;

namespace Application.Commands.Types.Delete;

public sealed class DeleteTypeCommandHandler : ICommandHandler<DeleteTypeCommand>
{
    private readonly ITypeRepository _typeRepository;

    public DeleteTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async ValueTask<Unit> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new InvalidOperationException($"Cannot find type with id {request.Id.Identity}");

        await _typeRepository.DeleteAsync(type, cancellationToken);

        return Unit.Value;
    }
}