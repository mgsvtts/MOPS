using Dapper;
using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
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
        var query = @$"INSERT INTO merch_items ({nameof(Models.MerchItem.id)},
                                                {nameof(Models.MerchItem.type_id)}, 
                                                {nameof(Models.MerchItem.name)}, 
                                                {nameof(Models.MerchItem.description)}, 
                                                {nameof(Models.MerchItem.price)}, 
                                                {nameof(Models.MerchItem.self_price)}, 
                                                {nameof(Models.MerchItem.amount)}, 
                                                {nameof(Models.MerchItem.created_at)})
                       VALUES (@{nameof(Models.MerchItem.id)},
                               @{nameof(Models.MerchItem.type_id)}, 
                               @{nameof(Models.MerchItem.name)}, 
                               @{nameof(Models.MerchItem.description)}, 
                               @{nameof(Models.MerchItem.price)}, 
                               @{nameof(Models.MerchItem.self_price)}, 
                               @{nameof(Models.MerchItem.amount)}, 
                               @{nameof(Models.MerchItem.created_at)})";

        var dbItem = _mapper.Map<Models.MerchItem>(item);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbItem);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllAsync()
    {
        var query = "SELECT * FROM merch_items";

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<Models.MerchItem>(query);

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllByIdsAsync(IEnumerable<MerchItemId> ids)
    {
        var query = "SELECT * FROM merch_items WHERE id IN @Ids";

        var queryIds = ids.Select(x => x.Identity.ToString()).ToArray();

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<Models.MerchItem>(query, new { Ids = queryIds });

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<Domain.MerchItemAggregate.MerchItem?> GetByIdAsync(MerchItemId id)
    {
        var query = "SELECT * FROM merch_items LIMIT(1)";

        using var connection = _db.CreateConnection();

        var item = await connection.QueryFirstOrDefaultAsync<Models.MerchItem>(query);

        return _mapper.Map<Domain.MerchItemAggregate.MerchItem>(item);
    }

    public async Task DeleteAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = $"DELETE FROM merch_items WHERE id = @{nameof(Models.MerchItem.id)}";

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { Id = item.Id.Identity.ToString() });
    }

    public async Task UpdateAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = @$"UPDATE merch_items SET 
                    {nameof(Models.MerchItem.type_id)} = @{nameof(Models.MerchItem.type_id)},
                    {nameof(Models.MerchItem.name)} = @{nameof(Models.MerchItem.name)},
                    {nameof(Models.MerchItem.description)} = @{nameof(Models.MerchItem.description)},
                    {nameof(Models.MerchItem.price)} = @{nameof(Models.MerchItem.price)},
                    {nameof(Models.MerchItem.self_price)} = @{nameof(Models.MerchItem.self_price)},
                    {nameof(Models.MerchItem.amount)} = @{nameof(Models.MerchItem.amount)} 
                     WHERE {nameof(Models.MerchItem.id)} = @{nameof(Models.MerchItem.id)}";

        var dbItem = _mapper.Map<Models.MerchItem>(item);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbItem);
    }
}
