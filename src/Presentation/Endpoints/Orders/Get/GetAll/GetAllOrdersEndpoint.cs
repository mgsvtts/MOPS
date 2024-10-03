using Application.Queries.Common;
using Application.Queries.Orders.GetAll;
using FastEndpoints;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.Orders.Get.GetAll;

public sealed class GetAllOrdersEndpoint(ISender _sender) : Endpoint<GetAllOrdersRequest, Pagination<GetAllOrdersResponseOrder>>
{
    public override void Configure()
    {
        Get("api/orders");
        Options(x => x.WithTags("Orders"));
    }

    public override async Task HandleAsync(GetAllOrdersRequest request, CancellationToken token)
    {
        Response = await _sender.Send
        (
            new GetAllOrdersQuery(new PaginationMeta
            (
                request.Page, 
                new PaginationApi
                (
                    HttpContext.Request.Path, 
                    HttpContext.Request.QueryString.ToString())
                )
            ),
            token
        );
    }
}
