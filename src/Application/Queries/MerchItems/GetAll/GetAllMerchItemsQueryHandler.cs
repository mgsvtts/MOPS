using Domain.MerchItemAggregate;
using Infrastructure.Common;
using LinqToDB;
using Mapster;
using Mediator;

namespace Application.Queries.MerchItems.GetAll;

public sealed class GetAllMerchItemsQueryHandler : IQueryHandler<GetAllMerchItemsQuery, List<MerchItem>>
{
    public async ValueTask<List<MerchItem>> Handle(GetAllMerchItemsQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        var items = await db.MerchItems
            .LoadWith(x => x.Images)
            .ToListAsync(cancellationToken);

        return items.Adapt<List<MerchItem>>();
    }
}