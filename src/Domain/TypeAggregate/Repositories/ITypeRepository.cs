using Domain.TypeAggregate.ValueObjects;

namespace Domain.TypeAggregate.Repositories;

public interface ITypeRepository
{
    public Task<Type> GetByIdAsync(TypeId id, CancellationToken token);

    public Task<List<Type>> GetAllAsync(CancellationToken token);

    public Task AddAsync(Type type, CancellationToken token);

    public Task DeleteAsync(Type type, CancellationToken token);

    public Task UpdateAsync(Type type, CancellationToken token);
}