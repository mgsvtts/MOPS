using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.MerchItemAggregate.Repositories;
public interface IMerchItemRepository
{
    public Task AddAsync(MerchItem item, CancellationToken cancellationToken = default);
    public Task<List<MerchItem>> GetAllAsync(CancellationToken token = default);
    public Task<MerchItem?> GetByIdAsync(MerchItemId id, CancellationToken token = default);
}
