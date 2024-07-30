using Application.Commands.MerchItems.Update;
using FastEndpoints;
using Mapster;
using Mediator;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Patch.Update;

public sealed class UpdateMerchItemEndpoint(ISender _sender) : Endpoint<UpdateMerchItemRequest, MerchItemDto>
{
    public override void Configure()
    {
        Patch("api/merch-items");
    }

    public override async Task HandleAsync(UpdateMerchItemRequest request, CancellationToken token)
    {
        var response = await _sender.Send(request.Adapt<UpdateMerchItemCommand>(), token);

        Response = response.Adapt<MerchItemDto>();
    }
}