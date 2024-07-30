using Application.Queries.Orders.Statistics;
using FastEndpoints;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.Orders.Get.GetStats;

public sealed class GetOrderStatsEndpoint(ISender _sender) : Endpoint<GetOrderStatisticQuery, GetOrderStatisticQueryResponse>
{
    public override void Configure()
    {
        Get("api/orders/stats");
        Options(x => x.WithTags("Orders"));
    }

    public override async Task HandleAsync(GetOrderStatisticQuery request, CancellationToken token)
    {
        Response = await _sender.Send(request, token);
    }
}