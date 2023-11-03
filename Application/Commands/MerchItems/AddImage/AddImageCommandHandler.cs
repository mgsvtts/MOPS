using Application.Commands.MerchItems.AddImage.Exceptions;
using Application.Commands.MerchItems.Common.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.AddImage;
public class AddImageCommandHandler : IRequestHandler<AddImageCommand>
{
    private readonly IImageRepository _imageRepository;

    public AddImageCommandHandler(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        if (request.Images.Count(x => x.Image.IsMain) > 1)
        {
            throw new OnlyOneMainImageAllowedException(request.Images.First().Image.MerchItemId);
        }

        await _imageRepository.AddAsync(request.Images.ToDictionary(key => key.Image, value => value.ImageStream));

        foreach (var item in request.Images.Select(x=>x.ImageStream))
        {
            await item.DisposeAsync();
        }
    }
}
