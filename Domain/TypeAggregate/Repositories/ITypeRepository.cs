using Domain.TypeAggregate.ValueObjects;

namespace Domain.TypeAggregate.Repositories;

public interface ITypeRepository
{
    public Task<Type> GetByIdAsync(TypeId id);

    public Task<List<Type>> GetAllAsync();

    public Task AddAsync(Type type);

    public Task DeleteAsync(Type type);

    public Task UpdateAsync(Type type);
}
