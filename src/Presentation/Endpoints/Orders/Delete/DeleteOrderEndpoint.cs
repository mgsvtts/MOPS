using Application.Commands.Orders.Delete;
using Domain.OrderAggregate.ValueObjects;
using FastEndpoints;
using Mediator;

namespace Presentation.Endpoints.Orders.Delete;

public sealed class DeleteOrderEndpoint(ISender _sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/orders/{orderId:guid}");
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        await _sender.Send(new DeleteOrderCommand(new OrderId(Route<Guid>("orderId"))), token);

        await SendNoContentAsync();
    }
}