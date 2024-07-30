using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure.Common;
using Infrastructure.Models;
using LinqToDB;
using Mapster;

namespace Infrastructure.Repositories;

public sealed class MerchItemRepository : IMerchItemRepository
{
    public async Task AddAsync(Domain.MerchItemAggregate.MerchItem item, CancellationToken token)
    {
        var dbItem = item.Adapt<MerchItem>();

        using var db = new DbConnection();

        await db.InsertOrReplaceAsync(dbItem, token: token);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllByIdsAsync(IEnumerable<MerchItemId> ids, CancellationToken token)
    {
        using var db = new DbConnection();

        var items = await db.MerchItems
            .Where(x => ids.Select(x => x.Identity).Contains(x.Id))
            .ToListAsync(token);

        return items.Adapt<List<Domain.MerchItemAggregate.MerchItem>>();
    }

    public async Task<Domain.MerchItemAggregate.MerchItem?> GetByIdAsync(MerchItemId id, CancellationToken token)
    {
        using var db = new DbConnection();

        var item = await db.MerchItems.FirstOrDefaultAsync(x => x.Id == id.Identity, token);

        return item.Adapt<Domain.MerchItemAggregate.MerchItem>();
    }

    public async Task DeleteAsync(Domain.MerchItemAggregate.MerchItem item, CancellationToken token)
    {
        using var db = new DbConnection();

        await db.MerchItems
            .Where(x => x.Id == item.Id.Identity)
            .DeleteAsync(token);
    }

    public async Task UpdateAsync(Domain.MerchItemAggregate.MerchItem item, CancellationToken token)
    {
        var dbItem = item.Adapt<MerchItem>();

        using var db = new DbConnection();

        await db.UpdateAsync(dbItem, token: token);
    }
}