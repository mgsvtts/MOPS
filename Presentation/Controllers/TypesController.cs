using Application.Commands.Types.CreateType;
using Application.Queries.Types.GetAllTypes;
using Contracts.Types;
using Contracts.Types.Create;
using Domain.MerchItemAggregate.Entities;
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
    public async Task Delete([FromRoute]Guid typeId, CancellationToken token)
    {
        
    }
}