﻿using Domain.MerchItemAggregate;
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
        var merchItem = request.Adapt<MerchItem>();

        await _merchItemRepository.AddAsync(merchItem, cancellationToken);

        if (!ImagesIsValid(request.Images))
        {
            return merchItem;
        }

        request.Images!.ForEach(x => x.Value.WithItemId(merchItem.Id));

        var images = await _imageRepository.AddAsync(request.Images.ToDictionary(key => key.Value, value => value.ImageStream), cancellationToken);

        return merchItem.WithImages(images);
    }

    private static bool ImagesIsValid(IEnumerable<CreateMerchItemCommandImage>? images)
    {
        if (images is null || !images.Any())
        {
            return false;
        }

        if (images.Any(x => x.ImageStream.Length == 0))
        {
            throw new InvalidOperationException("Some broken images found");
        }
        if (images.Where(x => x.Value.IsMain == true).Count() != 1)
        {
            throw new InvalidOperationException("Exaclty one main image allowed");
        }
        if (!images.Where(x => x.Value.IsMain == true).Any())
        {
            throw new InvalidOperationException("Set a default image");
        }

        return true;
    }
}