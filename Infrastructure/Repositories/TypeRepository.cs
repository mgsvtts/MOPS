using Dapper;
using Domain.TypeAggregate.Repositories;
using Domain.TypeAggregate.ValueObjects;
using Infrastructure.Misc.Queries;
using Infrastructure.Models;
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
        var query = Queries.Type.GetAll();

        using var connection = _db.CreateConnection();

        var items = await connection.QueryAsync<types>(query);

        return _mapper.Map<List<Domain.TypeAggregate.Type>>(items);
    }

    public async Task AddAsync(Domain.TypeAggregate.Type type)
    {
        var query = Queries.Type.Add();

        var dbType = _mapper.Map<types>(type);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbType);
    }

    public async Task DeleteAsync(Domain.TypeAggregate.Type type)
    {
        var query = Queries.Type.Delete();

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { id = type.Id.Identity.ToString() });
    }

    public async Task<Domain.TypeAggregate.Type> GetByIdAsync(TypeId id)
    {
        var query = Queries.Type.GetById();

        using var connection = _db.CreateConnection();

        var type = await connection.QueryFirstOrDefaultAsync<types>(query, new { id = id.Identity.ToString() });

        return _mapper.Map<Domain.TypeAggregate.Type>(type);
    }

    public async Task UpdateAsync(Domain.TypeAggregate.Type type)
    {
        var query = Queries.Type.Update();

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { name = type.Name.Value.ToString() });
    }
}
