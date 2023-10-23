using Dapper;
using Domain.TypeAggregate.Repositories;
using Domain.TypeAggregate.ValueObjects;
using MapsterMapper;

namespace Infrastructure.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly DbContext _db;
    private readonly IMapper _mapper;

    public TypeRepository(IMapper mapper, DbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<List<Domain.TypeAggregate.Type>> GetAllAsync()
    {
        var query = "SELECT * FROM types";

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<Models.Type>(query);

        return _mapper.Map<List<Domain.TypeAggregate.Type>>(items);
    }

    public async Task AddAsync(Domain.TypeAggregate.Type type)
    {
        var query = "INSERT INTO types (id, name)" +
                    "VALUES (@Id, @Name)";

        var parameters = new
        {
            Id = type.Id.Identity.ToString(),
            Name = type.Name.Value.ToString()
        };

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(Domain.TypeAggregate.Type type)
    {
        var query = "DELETE FROM types WHERE id = @Id";

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { Id = type.Id.Identity.ToString() });
    }

    public async Task<Domain.TypeAggregate.Type> GetByIdAsync(TypeId id)
    {
        var query = "SELECT * FROM types WHERE id = @Id";

        using var connection = _db.CreateConnection();

        var type = await connection.QueryFirstOrDefaultAsync<Models.Type>(query, new { Id = id.Identity.ToString() });

        return _mapper.Map<Domain.TypeAggregate.Type>(type);
    }

    public async Task UpdateAsync(Domain.TypeAggregate.Type type)
    {
        var query = "UPDATE types SET name = @Name";

        var parameters = new
        {
            Name = type.Name.Value.ToString()
        };

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, parameters);
    }
}
