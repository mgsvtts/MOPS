using Domain.MerchItemAggregate.Repositories;
using Mediator;

namespace Application.Commands.MerchItems.Delete;

public sealed class DeleteMerchItemCommandHandler(IMerchItemRepository _merchRepository,
                                                  IImageRepository _imageRepository) : ICommandHandler<DeleteMerchItemCommand>
{
    public async ValueTask<Unit> Handle(DeleteMerchItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _merchRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new InvalidOperationException($"Cannot find merch item with id {request.Id.Identity}");

        await Task.WhenAll(item.Images.Select(x => _imageRepository.DeleteAsync(x, cancellationToken)));
        await _merchRepository.DeleteAsync(item, cancellationToken);

        return Unit.Value;
    }
}