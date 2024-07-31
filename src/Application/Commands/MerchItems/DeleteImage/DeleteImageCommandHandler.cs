﻿using Domain.MerchItemAggregate.Repositories;
using Mediator;

namespace Application.Commands.MerchItems.DeleteImage;

public sealed class DeleteImageCommandHandler : ICommandHandler<DeleteImageCommand>
{
    private readonly IImageRepository _imageRepository;

    public DeleteImageCommandHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async ValueTask<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _imageRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Cannot find image with id {request.Id.Identity}");

        await _imageRepository.DeleteAsync(image, cancellationToken);

        if (image.IsMain)
        {
            await _imageRepository.UpdateMainImageAsync(cancellationToken);
        }

        return Unit.Value;
    }
}