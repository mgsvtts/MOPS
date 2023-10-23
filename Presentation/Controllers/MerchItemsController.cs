using Application.Commands.MerchItems.CalculateMerchItem;
using Application.Commands.MerchItems.CreateMerchItem;
using Application.Commands.MerchItems.DeleteMerchItem;
using Application.Commands.MerchItems.UpdateMerchItem;
using Application.Queries.MerchItems.GetAllMerchItems;
using Contracts.MerchItems;
using Contracts.MerchItems.Calculate;
using Contracts.MerchItems.Create;
using Contracts.MerchItems.Update;
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

    public MerchItemsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MerchItemDto>> GetAll(CancellationToken token)
    {
        var result = await _sender.Send(new GetAllMerchItemsQuery(), token);

        return _mapper.Map<IEnumerable<MerchItemDto>>(result);
    }

    [HttpGet("calculate")]
    public async Task<CalculateItemResponse> Calculate(IEnumerable<CalculateItemRequest> items, CancellationToken token = default)
    {
        var command = _mapper.Map<CalculateMerchItemCommand>(items);

        var result = await _sender.Send(command, token);

        return _mapper.Map<CalculateItemResponse>(result);
    }

    [HttpPost]
    public async Task<MerchItemDto> Create([FromForm] CreateMerchItemRequest request, CancellationToken token = default)
    {
        var command = _mapper.Map<CreateMerchItemCommand>(request);

        var item = await _sender.Send(command, token);

        return _mapper.Map<MerchItemDto>(item);
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
}
