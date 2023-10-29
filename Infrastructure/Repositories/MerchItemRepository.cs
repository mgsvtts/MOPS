using Dapper;
using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
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
        var query = @$"INSERT INTO {nameof(merch_items)} ({nameof(merch_items.id)},
                                                          {nameof(merch_items.type_id)},
                                                          {nameof(merch_items.name)},
                                                          {nameof(merch_items.description)},
                                                          {nameof(merch_items.price)},
                                                          {nameof(merch_items.self_price)},
                                                          {nameof(merch_items.amount)},
                                                          {nameof(merch_items.created_at)})
                       VALUES (@{nameof(merch_items.id)},
                               @{nameof(merch_items.type_id)},
                               @{nameof(merch_items.name)},
                               @{nameof(merch_items.description)},
                               @{nameof(merch_items.price)},
                               @{nameof(merch_items.self_price)},
                               @{nameof(merch_items.amount)},
                               @{nameof(merch_items.created_at)})";

        var dbItem = _mapper.Map<merch_items>(item);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbItem);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllAsync()
    {
        var query = $"SELECT * FROM {nameof(merch_items)}";

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<merch_items>(query);
 
        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllByIdsAsync(IEnumerable<MerchItemId> ids)
    {
        var query = $"SELECT * FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} IN @Ids";

        var queryIds = ids.Select(x => x.Identity.ToString()).ToArray();

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<merch_items>(query, new { Ids = queryIds });

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<Domain.MerchItemAggregate.MerchItem?> GetByIdAsync(MerchItemId id)
    {
        var query = $"SELECT * FROM {nameof(merch_items)} LIMIT(1)";

        using var connection = _db.CreateConnection();

        var item = await connection.QueryFirstOrDefaultAsync<merch_items>(query);

        return _mapper.Map<Domain.MerchItemAggregate.MerchItem>(item);
    }

    public async Task DeleteAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = $"DELETE FROM {nameof(merch_items)} WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)}";

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { Id = item.Id.Identity.ToString() });
    }

    public async Task UpdateAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = @$"UPDATE {nameof(merch_items)} SET
                    {nameof(merch_items.type_id)} = @{nameof(merch_items.type_id)},
                    {nameof(merch_items.name)} = @{nameof(merch_items.name)},
                    {nameof(merch_items.description)} = @{nameof(merch_items.description)},
                    {nameof(merch_items.price)} = @{nameof(merch_items.price)},
                    {nameof(merch_items.self_price)} = @{nameof(merch_items.self_price)},
                    {nameof(merch_items.amount)} = @{nameof(merch_items.amount)}
                     WHERE {nameof(merch_items.id)} = @{nameof(merch_items.id)}";

        var dbItem = _mapper.Map<merch_items>(item);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbItem);
    }
}
