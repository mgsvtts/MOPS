using Application.Queries.Common;
using Infrastructure.Common;
using Infrastructure.Models;
using LinqToDB;
using Mapster;
using Mediator;

namespace Application.Queries.Orders.GetAll;

public sealed class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, Pagination<GetAllOrdersResponseOrder>>
{
    public async ValueTask<Pagination<GetAllOrdersResponseOrder>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        var pagination = await db.Orders
            .LoadWith(x => x.OrderItems)
                .ThenLoad(x => x.MerchItem.Type)
            .LoadWith(x => x.OrderItems)
                .ThenLoad(x => x.Order)
            .PaginateAsync<Order, Guid>(request.Meta, cancellationToken);

        return pagination.MapItemsTo<GetAllOrdersResponseOrder>();
    }
}