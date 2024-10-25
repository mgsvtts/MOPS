﻿using Application.Commands.Types.Delete;
using Domain.TypeAggregate.ValueObjects;
using FastEndpoints;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.Types.Delete.DeleteType;

public sealed class DeleteTypeEndpoint(ISender _sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/types/{id:guid}");
        Options(x => x.WithTags("Types"));
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        await _sender.Send(new DeleteTypeCommand(new TypeId(Route<Guid>("id"))), token);

        await SendNoContentAsync();
    }
}