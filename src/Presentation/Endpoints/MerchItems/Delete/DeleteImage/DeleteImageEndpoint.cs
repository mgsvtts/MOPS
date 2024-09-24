using Application.Commands.MerchItems.DeleteImage;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using FastEndpoints;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.MerchItems.Delete.DeleteImage;

public sealed class DeleteImageEndpoint(ISender _sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/merch-items/image/{id:guid}");

        Options(x => x.WithTags("MerchItems"));
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        await _sender.Send(new DeleteImageCommand(new ImageId(Route<Guid>("id"))), token);

        await SendNoContentAsync();
    }
}