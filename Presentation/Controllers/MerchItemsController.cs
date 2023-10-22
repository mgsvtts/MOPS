using Application.Commands;
using Application.GetAllMerchItems;
using Contracts.MerchItem;
using Contracts.MerchItem.Create;
using Domain.MerchItemAggregate;
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

    [HttpPost]
    public async Task<MerchItemDto> Create([FromForm]CreateMerchItemRequest request, CancellationToken token = default)
    {
        var command = _mapper.Map<CreateMerchItemCommand>(request);

        var item = await _sender.Send(command, token);

        return _mapper.Map<MerchItemDto>(item);
    }
}
