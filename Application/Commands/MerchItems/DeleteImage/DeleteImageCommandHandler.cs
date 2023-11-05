using Domain.MerchItemAggregate.Repositories;
using MediatR;

namespace Application.Commands.MerchItems.DeleteImage;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
{
    private readonly IImageRepository _imageRepository;

    public DeleteImageCommandHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _imageRepository.GetByIdAsync(request.Id)
            ?? throw new InvalidOperationException($"Cannot find image with id {request.Id.Identity}");

        if (image.IsMain)
        {
            throw new InvalidOperationException("Cannot delete main image");
        }

        await _imageRepository.DeleteAsync(image);
    }
}
