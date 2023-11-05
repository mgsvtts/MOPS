using Domain.MerchItemAggregate.Repositories;
using MediatR;

namespace Application.Commands.MerchItems.Delete;

public class DeleteMerchItemCommandHandler : IRequestHandler<DeleteMerchItemCommand>
{
    private readonly IMerchItemRepository _merchRepository;
    private readonly IImageRepository _imageRepository;

    public DeleteMerchItemCommandHandler(IMerchItemRepository merchRepository, IImageRepository imageRepository)
    {
        _merchRepository = merchRepository;
        _imageRepository = imageRepository;
    }

    public async Task Handle(DeleteMerchItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _merchRepository.GetByIdAsync(request.Id)
                   ?? throw new InvalidOperationException($"Cannot find merch item with id {request.Id.Identity}");

        await Task.WhenAll(item.Images.Select(_imageRepository.DeleteAsync));
        await _merchRepository.DeleteAsync(item);
    }
}
