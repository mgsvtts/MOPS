namespace Domain.TypeAggregate.Repositories;

public interface ITypeRepository
{
    public Task<List<Type>> GetAllAsync(CancellationToken token = default);
    public Task AddAsync(Type type, CancellationToken token = default);
}