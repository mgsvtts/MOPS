using Application.Commands.Types.Create;
using Application.Commands.Types.Delete;
using Application.Commands.Types.Update;
using Application.Queries.Types.GetAll;
using Contracts.Types;
using Contracts.Types.Create;
using Contracts.Types.Update;
using Domain.TypeAggregate.ValueObjects;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Types;

[ApiController]
[Route("/api/types")]
public sealed class TypesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TypesController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<TypeDto>> GetAll(CancellationToken token)
    {
        var types = await _sender.Send(new GetAllTypesQuery(), token);

        return _mapper.Map<IEnumerable<TypeDto>>(types);
    }

    [HttpPost]
    public async Task<TypeDto> Create([FromForm] CreateTypeRequest request, CancellationToken token)
    {
        var command = _mapper.Map<CreateTypeCommand>(request);

        var result = await _sender.Send(command, token);

        return _mapper.Map<TypeDto>(result);
    }

    [HttpPatch]
    public async Task<TypeDto> Update([FromForm] UpdateTypeRequest request, CancellationToken token)
    {
        var command = _mapper.Map<UpdateTypeCommand>(request);

        var type = await _sender.Send(command, token);

        return _mapper.Map<TypeDto>(type);
    }

    [HttpDelete("{typeId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid typeId, CancellationToken token)
    {
        await _sender.Send(new DeleteTypeCommand(new TypeId(typeId)), token);

        return NoContent();
    }
}
