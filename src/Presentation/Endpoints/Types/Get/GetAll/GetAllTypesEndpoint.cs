﻿using Application.Queries.Types.GetAll;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.Types.Common;

namespace Presentation.Endpoints.Types.Get.GetAll;

public sealed class GetAllTypesEndpoint(ISender _sender) : EndpointWithoutRequest<IEnumerable<TypeDto>>
{
    public override void Configure()
    {
        Get("api/types");
        Options(x => x.WithTags("Types"));
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        var response = await _sender.Send(new GetAllTypesQuery(), token);

        Response = response.Adapt<IEnumerable<TypeDto>>();
    }
}