using Domain.TypeAggregate.Repositories;
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

    public async Task AddAsync(Domain.TypeAggregate.Type type,CancellationToken token = default)
    {
        var dbType = _mapper.Map<Models.Type>(type);

        await _db.AddAsync(dbType, token);

        await _db.SaveChangesAsync(token);
    }
}