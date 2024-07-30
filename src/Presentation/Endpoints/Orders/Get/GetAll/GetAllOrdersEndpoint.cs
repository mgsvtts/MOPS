using Application.Queries.Orders.GetAll;
using FastEndpoints;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.Orders.Get.GetAll;

public sealed class GetAllOrdersEndpoint(ISender _sender) : EndpointWithoutRequest<List<GetAllOrdersResponseOrder>>
{
    public override void Configure()
    {
        Get("api/orders");
        Options(x => x.WithTags("Orders"));
    }

    public override async Task HandleAsync(CancellationToken token)
    {
        Response = await _sender.Send(new GetAllOrdersQuery(), token);
    }
}