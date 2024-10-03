using Application.Queries.Common;
using Application.Queries.MerchItems.GetAll;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Get.GetAll;

public sealed class GetAllMerchItemsEndpoint(ISender _sender) : Endpoint<GetAllMerchItemsRequest, Pagination<MerchItemDto>>
{
    public override void Configure()
    {
        Get("api/merch-items");
        Options(x => x.WithTags("MerchItems"));
    }

    public override async Task HandleAsync(GetAllMerchItemsRequest request, CancellationToken token)
    {
        var response = await _sender.Send((HttpContext.Request, request).Adapt<GetAllMerchItemsQuery>(), token);

        Response = response.MapItemsTo<MerchItemDto>();
    }
}