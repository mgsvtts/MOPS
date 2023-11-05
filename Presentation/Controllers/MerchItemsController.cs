using Application.Commands.MerchItems.Calculate;
using Application.Commands.MerchItems.Create;
using Application.Commands.MerchItems.Delete;
using Application.Commands.MerchItems.DeleteImage;
using Application.Commands.MerchItems.Update;
using Application.Queries.MerchItems.GetAll;
using Contracts.MerchItems;
using Contracts.MerchItems.Calculate;
using Contracts.MerchItems.Create;
using Contracts.MerchItems.Update;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/merch")]
public class MerchItemsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IImageRepository _imageRepo;

    public MerchItemsController(ISender sender, IMapper mapper, IImageRepository imageRepo)
    {
        _sender = sender;
        _mapper = mapper;
        _imageRepo = imageRepo;
    }

    [HttpGet]
    public async Task<IEnumerable<MerchItemDto>> GetAll(bool showNotAvailable = true, CancellationToken token = default)
    {
        var result = await _sender.Send(new GetAllMerchItemsQuery(showNotAvailable), token);

        return _mapper.Map<IEnumerable<MerchItemDto>>(result);
    }

    [HttpPost]
    public async Task<MerchItemDto> Create([FromForm] CreateMerchItemRequest request, CancellationToken token = default)
    {
        var command = _mapper.Map<CreateMerchItemCommand>(request);

        if (request.Images?.Any() == true)
        {
            command = SetImages(request, command);
        }

        var item = await _sender.Send(command, token);

        return _mapper.Map<MerchItemDto>(item);
    }

    [HttpPost("calculate")]
    public async Task<CalculateItemResponse> Calculate(IEnumerable<CalculateItemRequest> items, CancellationToken token = default)
    {
        var command = _mapper.Map<CalculateMerchItemCommand>(items);

        var result = await _sender.Send(command, token);

        return _mapper.Map<CalculateItemResponse>(result);
    }

    [HttpPatch]
    public async Task<MerchItemDto> Update([FromForm] UpdateMerchItemRequest request, CancellationToken token)
    {
        var command = _mapper.Map<UpdateMerchItemCommand>(request);

        var result = await _sender.Send(command, token);

        return _mapper.Map<MerchItemDto>(result);
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid itemId, CancellationToken token)
    {
        await _sender.Send(new DeleteMerchItemCommand(new MerchItemId(itemId)), token);

        return NoContent();
    }

    [HttpDelete("delete-image/{imageId}")]
    public async Task DeleteImage([FromRoute] Guid imageId, CancellationToken token)
    {
        var command = new DeleteImageCommand(new ImageId(imageId));

        await _sender.Send(command, token);
    }

    private static CreateMerchItemCommand SetImages(CreateMerchItemRequest request, CreateMerchItemCommand command)
    {
        command = command with
        {
            Images = request.Images!.Select(x => new CreateMerchItemCommandImage(new Image(new ImageId(),
                                                                                           new MerchItemId(),
                                                                                           isMain: x.IsMain),
                                                                                 x.File.OpenReadStream())).ToList()
        };
        return command;
    }
}
