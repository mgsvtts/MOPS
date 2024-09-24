using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Mapster;
using Mediator;

namespace Application.Commands.MerchItems.Create;

public sealed class CreateMerchItemCommandHandler : ICommandHandler<CreateMerchItemCommand, MerchItem>
{
    private readonly IMerchItemRepository _merchItemRepository;
    private readonly IImageRepository _imageRepository;

    public CreateMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
                                         IImageRepository imageRepository)
    {
        _merchItemRepository = merchItemRepository;
        _imageRepository = imageRepository;
    }

    public async ValueTask<MerchItem> Handle(CreateMerchItemCommand request, CancellationToken cancellationToken)
    {
        ValidateImages(request);

        var merchItem = request.Adapt<MerchItem>();

        await _merchItemRepository.AddAsync(merchItem, cancellationToken);

        if (request.Images is null)
        {
            return merchItem;
        }

        request.Images.ForEach(x => x.Value.WithItemId(merchItem.Id));

        var images = await _imageRepository.AddAsync(request.Images.ToDictionary(key => key.Value, value => value.ImageStream), cancellationToken);

        return merchItem.WithImages(images);
    }

    private static void ValidateImages(CreateMerchItemCommand request)
    {
        if (request.Images is null || request.Images.Count == 0)
        {
            return;
        }

        if (request.Images.Any(x => x.ImageStream.Length == 0))
        {
            throw new InvalidOperationException("Some broken images found");
        }
        if (request.Images.Where(x => x.Value.IsMain == true).Count() != 1)
        {
            throw new InvalidOperationException("Exaclty one main image allowed");
        }
        if (!request.Images.Where(x => x.Value.IsMain == true).Any())
        {
            throw new InvalidOperationException("Set a default image");
        }
    }
}