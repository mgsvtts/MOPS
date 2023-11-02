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
        await _imageRepository.AddAsync(request.Images);
    }
}
