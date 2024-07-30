using Application.Commands.MerchItems.DeleteImage;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using FastEndpoints;
using Mediator;

namespace Presentation.Endpoints.MerchItems.Delete.DeleteImage;

public sealed class DeleteImageEndpoint(ISender _sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/merch-items/image{imageId:guid}");
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        await _sender.Send(new DeleteImageCommand(new ImageId(Route<Guid>("imageId"))), token);

        await SendNoContentAsync();
    }
}