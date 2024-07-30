using Infrastructure.Common;
using LinqToDB;
using Mapster;
using Mediator;

namespace Application.Queries.Orders.GetAll;

public sealed class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, List<GetAllOrdersResponseOrder>>
{
    public async ValueTask<List<GetAllOrdersResponseOrder>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        var dbOrders = await db.Orders
            .LoadWith(x => x.OrderItems)
                .ThenLoad(x => x.MerchItem.Type)
            .LoadWith(x => x.OrderItems)
                .ThenLoad(x => x.Order)
            .ToListAsync(cancellationToken);

        return dbOrders.Adapt<List<GetAllOrdersResponseOrder>>();
    }
}