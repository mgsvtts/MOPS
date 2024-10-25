using Domain.TypeAggregate.Repositories;
using Domain.TypeAggregate.ValueObjects;
using Infrastructure.Common;
using LinqToDB;
using Mapster;
using Type = Infrastructure.Models.Type;

namespace Infrastructure.Repositories;

public sealed class TypeRepository : ITypeRepository
{
    public async Task AddAsync(Domain.TypeAggregate.Type type, CancellationToken token)
    {
        using var db = new DbConnection();

        var dbType = type.Adapt<Type>();

        await db.InsertOrReplaceAsync(dbType, token: token);
    }

    public async Task DeleteAsync(Domain.TypeAggregate.Type type, CancellationToken token)
    {
        using var db = new DbConnection();

        await db.Types
            .Where(x => x.Id == type.Id.Identity)
            .DeleteAsync(token);
    }

    public async Task<Domain.TypeAggregate.Type> GetByIdAsync(TypeId id, CancellationToken token)
    {
        using var db = new DbConnection();

        var type = await db.Types.FirstOrDefaultAsync(x => x.Id == id.Identity, token: token);

        return type.Adapt<Domain.TypeAggregate.Type>();
    }

    public async Task UpdateAsync(Domain.TypeAggregate.Type type, CancellationToken token)
    {
        using var db = new DbConnection();

        var dbType = type.Adapt<Type>();

        await db.InsertOrReplaceAsync(dbType, token: token);
    }
}