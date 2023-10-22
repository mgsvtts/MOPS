using Domain.TypeAggregate.ValueObjects;

namespace Domain.TypeAggregate.Repositories;

public interface ITypeRepository
{
    public Task<Type> GetByIdAsync(TypeId id, CancellationToken token = default);
    public Task<List<Type>> GetAllAsync(CancellationToken token = default);
    public Task AddAsync(Type type, CancellationToken token = default);
    public Task DeleteAsync(Type type, CancellationToken token = default);
    public Task<Type> UpdateAsync(Type type, CancellationToken token = default);
}