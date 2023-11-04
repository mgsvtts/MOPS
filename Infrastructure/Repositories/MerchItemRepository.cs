using Dapper;
using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure.Misc.Queries;
using Infrastructure.Models;
using MapsterMapper;

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

    public async Task AddAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = Queries.MerchItem.AddMerchItem();

        var dbItem = _mapper.Map<merch_items>(item);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbItem);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllAsync(bool showNotAvailable = true)
    {
        var query = Queries.MerchItem.GetAllMerchItems(showNotAvailable);

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<merch_items>(query);

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllByIdsAsync(IEnumerable<MerchItemId> ids)
    {
        var query = Queries.MerchItem.GetAllByIds();

        var queryIds = ids.Select(x => x.Identity.ToString()).ToArray();

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<merch_items>(query, new { Ids = queryIds });

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<Domain.MerchItemAggregate.MerchItem?> GetByIdAsync(MerchItemId id)
    {
        var query = Queries.MerchItem.GetById();

        using var connection = _db.CreateConnection();

        var item = await connection.QueryFirstOrDefaultAsync<merch_items>(query, new { id = id.Identity.ToString() });

        return _mapper.Map<Domain.MerchItemAggregate.MerchItem>(item);
    }

    public async Task DeleteAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = Queries.MerchItem.Delete();

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { id = item.Id.Identity.ToString() });
    }

    public async Task UpdateAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = Queries.MerchItem.Update();

        var dbItem = _mapper.Map<merch_items>(item);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbItem);
    }
}
