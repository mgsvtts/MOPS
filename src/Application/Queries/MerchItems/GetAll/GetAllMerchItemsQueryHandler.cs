using Domain.MerchItemAggregate;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Models;
using LinqToDB;
using MapsterMapper;
using Mediator;
using MerchItem = Infrastructure.Models.MerchItem;

namespace Application.Queries.MerchItems.GetAll;

public sealed class GetAllMerchItemsQueryHandler : IQueryHandler<GetAllMerchItemsQuery, List<MerchItem>>
{
    public async ValueTask<List<MerchItem>> Handle(GetAllMerchItemsQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        return await db.MerchItems
            .LoadWith(x => x.Images)
            .ToListAsync(cancellationToken);
    }
}
