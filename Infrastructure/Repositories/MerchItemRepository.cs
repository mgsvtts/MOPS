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
        var query = "INSERT INTO merch_items (id, type_id, name, description, price, self_price, amount)" +
                    "VALUES (@Id, @TypeId, @Name, @Description, @Price, @SelfPrice, @Amount)";

        var parameters = new
        {
            Id = item.Id.Identity.ToString(),
            TypeId = item.TypeId.Identity.ToString(),
            Name = item.Name.Value,
            Description = item.Description?.Value,
            Price = item.Price.Value.ToString(),
            SelfPrice = item.SelfPrice.Value.ToString(),
            Amount = item.AmountLeft.Value.ToString()
        };

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
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
        var query = "DELETE FROM merch_items WHERE id = @Id";

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { Id = item.Id.Identity.ToString() });
    }

    public async Task UpdateAsync(Domain.MerchItemAggregate.MerchItem item)
    {
        var query = "UPDATE merch_items SET " +
                    "type_id = @TypeId," +
                    "name = @Name," +
                    "description = @Description," +
                    "price = @Price," +
                    "self_price = @SelfPrice," +
                    "amount = @Amount " +
                    "WHERE id = @Id";

        var parameters = new
        {
            Id = item.Id.Identity.ToString(),
            TypeId = item.TypeId.Identity.ToString(),
            Name = item.Name.Value,
            Description = item.Description?.Value,
            Price = item.Price.Value.ToString(),
            SelfPrice = item.SelfPrice.Value.ToString(),
            Amount = item.AmountLeft.Value.ToString()
        };

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
    }
}
