using Application.Commands.Orders.Create;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.Orders.Get.GetAll;

namespace Presentation.Endpoints.Orders.Post.Create;

public sealed class CreateOrderEndpoint(ISender _sender) : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    public override void Configure()
    {
        Post("api/orders");
        Options(x => x.WithTags("Orders"));
    }

    public override async Task HandleAsync(CreateOrderRequest request, CancellationToken token)
    {
        var result = await _sender.Send(request.Adapt<CreateOrderCommand>(), token);

        await SendCreatedAtAsync<GetAllOrdersEndpoint>(null, result.Adapt<CreateOrderResponse>());
    }
}