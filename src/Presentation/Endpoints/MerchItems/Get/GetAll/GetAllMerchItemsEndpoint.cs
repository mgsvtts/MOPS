using Application.Queries.MerchItems.GetAll;
using FastEndpoints;
using Mapster;
using Mediator;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Get.GetAll;

public sealed class GetAllMerchItemsEndpoint(ISender _sender) : Endpoint<GetAllMerchItemsRequest, IEnumerable<MerchItemDto>>
{
    public override void Configure()
    {
        Get("api/merch-items");
    }

    public override async Task HandleAsync(GetAllMerchItemsRequest request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<GetAllMerchItemsQuery>(), token);

        Response = response.Adapt<IEnumerable<MerchItemDto>>();
    }
}