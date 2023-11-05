using Domain.MerchItemAggregate.Repositories;
using MediatR;

namespace Application.Commands.MerchItems.AddImage;

public class AddImageCommandHandler : IRequestHandler<AddImageCommand>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMerchItemRepository _merchItemRepository;

    public AddImageCommandHandler(IImageRepository imageRepository, IMerchItemRepository merchItemRepository)
    {
        _imageRepository = imageRepository;
        _merchItemRepository = merchItemRepository;
    }

    public async Task Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var itemId = request.Images.First().Image.MerchItemId;
        if (request.Images.Count(x => x.Image.IsMain) > 1)
        {
            throw new InvalidOperationException("Only one main image allowed");
        }

        var merchItem = await _merchItemRepository.GetByIdAsync(itemId)
            ?? throw new InvalidOperationException($"Merch item with id {itemId.Identity} not found");

        var mainImage = merchItem.Images.FirstOrDefault(x => x.IsMain);
        if (request.Images.Any(x => x.Image.IsMain) && mainImage is not null)
        {
            mainImage.NotMain();

            await _imageRepository.UpdateAsync(mainImage);
        }

        await _imageRepository.AddAsync(request.Images.ToDictionary(key => key.Image, value => value.ImageStream));

        foreach (var item in request.Images.Select(x => x.ImageStream))
        {
            await item.DisposeAsync();
        }
    }
}
