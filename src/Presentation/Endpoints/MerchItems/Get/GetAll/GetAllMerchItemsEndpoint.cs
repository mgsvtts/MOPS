using Application.Queries.MerchItems.GetAll;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Get.GetAll;

public sealed class GetAllMerchItemsEndpoint(ISender _sender) : Endpoint<GetAllMerchItemsRequest, List<MerchItemDto>>
{
    public override void Configure()
    {
        Get("api/merch-items");
        Options(x => x.WithTags("MerchItems"));
    }

    public override async Task HandleAsync(GetAllMerchItemsRequest request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<GetAllMerchItemsQuery>(), token);

        Response = response.Adapt<List<MerchItemDto>>();
    }
}