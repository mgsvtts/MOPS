using Dapper;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure.Models;
using MapsterMapper;
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
        // var dbItem = _mapper.Map<Models.MerchItem>(item);

        // await _db.AddAsync(dbItem, cancellationToken);

        // await _db.SaveChangesAsync(cancellationToken);

        throw new Exception();
    }

    public async Task<List<Domain.MerchItemAggregate.MerchItem>> GetAllAsync(CancellationToken token = default)
    {
        var query = "SELECT * FROM merch_items";

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<Models.MerchItem>(query);

        return _mapper.Map<List<Domain.MerchItemAggregate.MerchItem>>(items);
    }

    public async Task<Domain.MerchItemAggregate.MerchItem?> GetByIdAsync(MerchItemId id, CancellationToken token = default)
    {
        var query = "SELECT * FROM merch_items LIMIT(1)";

        using var connection = _db.CreateConnection();

        var item = await connection.QueryFirstOrDefaultAsync<Models.MerchItem>(query);

        return _mapper.Map<Domain.MerchItemAggregate.MerchItem>(item);
    }

    public async Task DeleteAsync(Domain.MerchItemAggregate.MerchItem item, CancellationToken token = default)
    {
        var query = "DELETE FROM merch_items WHERE id = @Id";

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { Id = item.Id.Identity.ToString() });
    }
}
