using Application.Queries.Common;
using Application.Queries.Types.GetAll;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.Types.Common;

namespace Presentation.Endpoints.Types.Get.GetAll;

public sealed class GetAllTypesEndpoint(ISender _sender) : Endpoint<GetAllTypesRequest, Pagination<TypeDto>>
{
    public override void Configure()
    {
        Get("api/types");
        Options(x => x.WithTags("Types"));
    }

    public override async Task HandleAsync(GetAllTypesRequest request, CancellationToken token)
    {
        var response = await _sender.Send
        (
            new GetAllTypesQuery
            (
                new PaginationMeta
                (
                    request.Page, 
                    new PaginationApi
                    (
                        HttpContext.Request.Path,
                                    HttpContext.Request.QueryString.ToString()
                    )
                )
            ), token
        );

        Response = response.MapItemsTo<TypeDto>();
    }
}
