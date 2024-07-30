using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Post.Calculate;

public sealed class CalculateMerchItemEndpoint(ISender _sender) : Endpoint<IEnumerable<CalculateMerchItemRequest>, CalculateMerchItemResponse>
{
    public override void Configure()
    {
        Post("api/merch-items/calculate");
        Options(x => x.WithTags("MerchItems"));
    }

    public override async Task HandleAsync(IEnumerable<CalculateMerchItemRequest> request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<MerchItemDto>(), token);

        Response = response.Adapt<CalculateMerchItemResponse>();
    }
}