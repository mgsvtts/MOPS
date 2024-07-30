using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.MerchItemAggregate.Repositories;

public interface IMerchItemRepository
{
    public Task AddAsync(MerchItem item, CancellationToken token);

    public Task<MerchItem?> GetByIdAsync(MerchItemId id, CancellationToken token);

    public Task DeleteAsync(MerchItem id, CancellationToken token);

    public Task UpdateAsync(MerchItem item, CancellationToken token);

    public Task<List<MerchItem>> GetAllByIdsAsync(IEnumerable<MerchItemId> ids, CancellationToken token);
}