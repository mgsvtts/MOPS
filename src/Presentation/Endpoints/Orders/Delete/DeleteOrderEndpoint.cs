using Application.Commands.Orders.Delete;
using Domain.OrderAggregate.ValueObjects;
using FastEndpoints;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.Orders.Delete;

public sealed class DeleteOrderEndpoint(ISender _sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/orders/{orderId:guid}");
        Options(x => x.WithTags("Orders"));
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        await _sender.Send(new DeleteOrderCommand(new OrderId(Route<Guid>("orderId"))), token);

        await SendNoContentAsync();
    }
}