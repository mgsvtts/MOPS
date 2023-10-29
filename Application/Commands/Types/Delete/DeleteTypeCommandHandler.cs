using Domain.TypeAggregate.Repositories;
using MediatR;

namespace Application.Commands.Types.Delete;

public class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeCommand>
{
    private readonly ITypeRepository _typeRepository;

    public DeleteTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _typeRepository.GetByIdAsync(request.Id)
                   ?? throw new InvalidOperationException($"Cannot find type with id {request.Id.Identity}");

        await _typeRepository.DeleteAsync(type);
    }
}
