using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate.Repositories;

public interface IOrderRepository
{
    public Task AddAsync(Order order);
    public Task DeleteAsync(Order order);
    public Task<Order?> GetByIdAsync(OrderId id);
}
