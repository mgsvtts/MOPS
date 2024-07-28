using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate.Repositories;

public interface IOrderRepository
{
    public Task AddAsync(Order order, CancellationToken token);

    public Task DeleteAsync(Order order, CancellationToken token);

    public Task<Order?> GetByIdAsync(OrderId id, CancellationToken token);
}
