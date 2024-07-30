using Application.Commands.MerchItems.Delete;
using Domain.MerchItemAggregate.ValueObjects;
using FastEndpoints;
using Mediator;

namespace Presentation.Endpoints.MerchItems.Delete.DeleteMerchItem;

public sealed class DeleteMerchItemEndpoint(ISender _sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/merch-items/{merchItemId:guid}");
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        await _sender.Send(new DeleteMerchItemCommand(new MerchItemId(Route<Guid>("merchItemId"))), token);

        await SendNoContentAsync();
    }
}