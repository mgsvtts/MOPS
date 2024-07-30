using Application.Commands.Types.Create;
using FastEndpoints;
using Mapster;
using Mediator;
using Presentation.Endpoints.Types.Common;

namespace Presentation.Endpoints.Types.Post.Create;

public sealed class CreateTypeEndpoint(ISender _sender) : Endpoint<CreateTypeRequest, TypeDto>
{
    public override void Configure()
    {
        Post("api/types");
    }

    public override async Task HandleAsync(CreateTypeRequest request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<CreateTypeCommand>(), token);

        Response = response.Adapt<TypeDto>();
    }
}