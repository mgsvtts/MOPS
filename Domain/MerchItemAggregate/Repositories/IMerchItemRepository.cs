using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.MerchItemAggregate.Repositories;

public interface IMerchItemRepository
{
    public Task AddAsync(MerchItem item);

    public Task<MerchItem?> GetByIdAsync(MerchItemId id);

    public Task DeleteAsync(MerchItem id);

    public Task UpdateAsync(MerchItem item);

    public Task<List<MerchItem>> GetAllByIdsAsync(IEnumerable<MerchItemId> ids);
}
