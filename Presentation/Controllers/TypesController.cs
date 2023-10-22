using Application.Commands.Types.CreateType;
using Application.Commands.Types.DeleteType;
using Application.Commands.Types.UpdateType;
using Application.Queries.Types.GetAllTypes;
using Contracts.Types;
using Contracts.Types.Create;
using Contracts.Types.Update;
using Domain.MerchItemAggregate.Entities;
using Domain.TypeAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/types")]
public class TypesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TypesController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost]
    public async Task<TypeDto> Create([FromForm] CreateTypeRequest request, CancellationToken token)
    {
        var command = _mapper.Map<CreateTypeCommand>(request);

        var result = await _sender.Send(command, token);

        return _mapper.Map<TypeDto>(result);
    }

    [HttpGet]
    public async Task<IEnumerable<TypeDto>> GetAll(CancellationToken token)
    {
        var types = await _sender.Send(new GetAllTypesQuery(), token);

        return _mapper.Map<IEnumerable<TypeDto>>(types);
    }

    [HttpDelete("{typeId}")]
    public async Task Delete([FromRoute] Guid typeId, CancellationToken token)
    {
        await _sender.Send(new DeleteTypeCommand(new TypeId(typeId)), token);
    }

    [HttpPatch]
    public async Task Update([FromForm] UpdateTypeRequest request, CancellationToken token)
    {
        var command = _mapper.Map<UpdateTypeCommand>(request);
        
        await _sender.Send(command, token);
    }
}