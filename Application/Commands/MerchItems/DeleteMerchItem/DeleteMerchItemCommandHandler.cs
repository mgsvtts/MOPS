using Domain.MerchItemAggregate.Repositories;
using MediatR;

namespace Application.Commands.MerchItems.DeleteMerchItem;

public class DeleteMerchItemCommandHandler : IRequestHandler<DeleteMerchItemCommand>
{
    private readonly IMerchItemRepository _merchRepository;

    public DeleteMerchItemCommandHandler(IMerchItemRepository merchRepository)
    {
        _merchRepository = merchRepository;
    }

    public async Task Handle(DeleteMerchItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _merchRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new InvalidOperationException($"Cannot find merch item with id {request.Id.Identity}");

        await _merchRepository.DeleteAsync(item, cancellationToken);
    }
}