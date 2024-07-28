using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using MapsterMapper;
using Mediator;

namespace Application.Commands.MerchItems.Create;

public sealed class CreateMerchItemCommandHandler : ICommandHandler<CreateMerchItemCommand, MerchItem>
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

    public async ValueTask<MerchItem> Handle(CreateMerchItemCommand request, CancellationToken cancellationToken)
    {
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
