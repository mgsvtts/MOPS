using Application.Commands.Types.Create;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.Types.Common;
using Presentation.Endpoints.Types.Get.GetAll;

namespace Presentation.Endpoints.Types.Post.Create;

public sealed class CreateTypeEndpoint(ISender _sender) : Endpoint<CreateTypeRequest, TypeDto>
{
    public override void Configure()
    {
        Post("api/types");
        Options(x => x.WithTags("Types"));
    }

    public override async Task HandleAsync(CreateTypeRequest request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<CreateTypeCommand>(), token);

        await SendCreatedAtAsync<GetAllTypesEndpoint>(null, response.Adapt<TypeDto>());
    }
}