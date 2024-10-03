using Application.Queries.Common;
using Domain.MerchItemAggregate;
using Infrastructure.Common;
using LinqToDB;
using Mapster;
using Mediator;

namespace Application.Queries.MerchItems.GetAll;

public sealed class GetAllMerchItemsQueryHandler : IQueryHandler<GetAllMerchItemsQuery, Pagination<MerchItem>>
{
    public async ValueTask<Pagination<MerchItem>> Handle(GetAllMerchItemsQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        var pagination = await db.MerchItems
            .LoadWith(x => x.Images)
            .Sort(request.Sort)
            .PaginateAsync<Infrastructure.Models.MerchItem, Guid>(request.Meta, cancellationToken);

        return pagination.MapItemsTo<MerchItem>();
    }
}


public static class MerchItemsQueryExtensions
{
    public static IQueryable<Infrastructure.Models.MerchItem> Sort(this IQueryable<Infrastructure.Models.MerchItem> items, MerchItemSort sort)
    {
        return sort switch
        {
            MerchItemSort.NameAsc => items.OrderBy(x => x.Name),
            MerchItemSort.NameDesc => items.OrderByDescending(x => x.Name),
            MerchItemSort.DescriptionAsc => items.OrderBy(x => x.Description),
            MerchItemSort.DescriptionDesc => items.OrderByDescending(x => x.Description),
            MerchItemSort.PriceAsc => items.OrderBy(x => x.Price),
            MerchItemSort.PriceDesc => items.OrderByDescending(x => x.Price),
            MerchItemSort.SelfPriceAsc => items.OrderBy(x => x.SelfPrice),
            MerchItemSort.SelfPriceDesc => items.OrderByDescending(x => x.SelfPrice),
            MerchItemSort.AmountAsc => items.OrderBy(x => x.Amount),
            MerchItemSort.AmountDesc => items.OrderByDescending(x => x.Amount),
            MerchItemSort.CreatedAsc => items.OrderBy(x => x.CreatedAt),
            MerchItemSort.CreatedDesc => items.OrderByDescending(x => x.CreatedAt),
            _ => items 
        };
    }
}