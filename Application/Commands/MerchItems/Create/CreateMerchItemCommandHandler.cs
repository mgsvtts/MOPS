using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Infrastructure.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Commands.MerchItems.Create;

internal class CreateMerchItemCommandHandler : IRequestHandler<CreateMerchItemCommand, MerchItem>
{
    private readonly IMapper _mapper;
    private readonly IMerchItemRepository _merchItemRepository;
    private readonly IImageRepository _imageRepository;

    public CreateMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
                                         IMapper mapper,
                                         IImageRepository imageRepository)
    {
        _merchItemRepository = merchItemRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
    }

    public async Task<MerchItem> Handle(CreateMerchItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Images.Any(x => x.ImageStream.Length == 0))
        {
            throw new InvalidOperationException("Some broken images found");
        }
        if (request.Images.Select(x => x.Value.IsMain).Count() > 1)
        {
            throw new InvalidOperationException("Only one main image allowed");
        }
        if (!request.Images.Select(x => x.Value.IsMain).Any())
        {
            throw new InvalidOperationException("Set a default image");
        }

        var merchItem = _mapper.Map<MerchItem>(request);

        request.Images.ForEach(x => x.Value.WithItemId(merchItem.Id));

        await _merchItemRepository.AddAsync(merchItem);

        await _imageRepository.AddAsync(request.Images.ToDictionary(key => key.Value, value => value.ImageStream));

        foreach (var item in request.Images.Select(x => x.ImageStream))
        {
            await item.DisposeAsync();
        }

        return merchItem.WithImages(request.Images.Select(x => x.Value));
    }
}
