using FastEndpoints;
using Mapster;
using Mediator;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Post.Calculate;

public sealed class CalculateMerchItemsEndpoint(ISender _sender) : Endpoint<IEnumerable<CalculateItemRequest>, CalculateItemResponse>
{
    public override void Configure()
    {
        Post("api/merch-items/calculate");
    }

    public override async Task HandleAsync(IEnumerable<CalculateItemRequest> request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<MerchItemDto>(), token);

        Response = response.Adapt<CalculateItemResponse>();
    }
}