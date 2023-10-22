using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class MerchItemRepository : IMerchItemRepository
{
    private readonly DbContext _db;
    private readonly IMapper _mapper;

    public MerchItemRepository(IMapper mapper, DbContext db)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task AddAsync(Domain.MerchItemAggregate.MerchItem item, CancellationToken cancellationToken = default)
    {
        var dbItem = _mapper.Map<Models.MerchItem>(item);

        await _db.AddAsync(dbItem, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllAsync(CancellationToken token = default)
    {
        var items = await _db.MerchItems.ToListAsync(token);

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<Domain.MerchItemAggregate.MerchItem?> GetByIdAsync(MerchItemId id, CancellationToken token = default)
    {
        var item = await _db.MerchItems.FindAsync(new object?[] { id.Identity.ToString() }, cancellationToken: token);

        return _mapper.Map<Domain.MerchItemAggregate.MerchItem>(item);
    }

    public async Task DeleteAsync(Domain.MerchItemAggregate.MerchItem item, CancellationToken token = default)
    {
        var dbItem = _mapper.Map<Models.MerchItem>(item);

        _db.MerchItems.Remove(dbItem);

        await _db.SaveChangesAsync(token);
    }
}
