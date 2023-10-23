using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.MerchItemAggregate.Repositories;

public interface IMerchItemRepository
{
    public Task AddAsync(MerchItem item);

    public Task<List<MerchItem>> GetAllAsync();

    public Task<MerchItem?> GetByIdAsync(MerchItemId id);

    public Task DeleteAsync(MerchItem id);

    public Task UpdateAsync(MerchItem item);
}
