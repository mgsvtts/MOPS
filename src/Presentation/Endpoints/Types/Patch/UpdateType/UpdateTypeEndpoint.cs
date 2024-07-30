﻿using Application.Commands.Types.Update;
using FastEndpoints;
using Mapster;
using Mediator;
using Presentation.Endpoints.Types.Common;

namespace Presentation.Endpoints.Types.Patch.UpdateType;

public sealed class UpdateTypeEndpoint(ISender _sender) : Endpoint<UpdateTypeRequest, TypeDto>
{
    public override void Configure()
    {
        Patch("api/types");
    }

    public override async Task HandleAsync(UpdateTypeRequest request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<UpdateTypeCommand>(), token);

        Response = response.Adapt<TypeDto>();
    }
}