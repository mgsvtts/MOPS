using Domain.TypeAggregate;
using Domain.TypeAggregate.Repositories;
using Domain.TypeAggregate.ValueObjects;
using Infrastructure.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Domain.TypeAggregate.Type>> GetAllAsync(CancellationToken token = default)
    {
        var types = await _db.Types.ToListAsync(token);

        return _mapper.Map<List<Domain.TypeAggregate.Type>>(types);
    }

    public async Task AddAsync(Domain.TypeAggregate.Type type, CancellationToken token = default)
    {
        var dbType = _mapper.Map<Models.Type>(type);

        await _db.AddAsync(dbType, token);

        await _db.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Domain.TypeAggregate.Type type, CancellationToken token = default)
    {
        var dbType = _mapper.Map<Models.Type>(type);

        _db.Types.Remove(dbType);

        await _db.SaveChangesAsync(token);
    }

    public async Task<Domain.TypeAggregate.Type> GetByIdAsync(TypeId id, CancellationToken token = default)
    {
        var type = await _db.Types.FindAsync(new object?[] { id.Identity.ToString() }, cancellationToken: token);

        return _mapper.Map<Domain.TypeAggregate.Type>(type);
    }

    public async Task<Domain.TypeAggregate.Type> UpdateAsync(Domain.TypeAggregate.Type type, CancellationToken token = default)
    {
        var dbType = _mapper.Map<Models.Type>(type);

        _db.Types.Update(dbType);

        await _db.SaveChangesAsync(token);

        return _mapper.Map<Domain.TypeAggregate.Type>(dbType);
    }
}