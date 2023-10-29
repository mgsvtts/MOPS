﻿namespace Domain.OrderAggregate.Repositories;

public interface IOrderRepository
{
    public Task AddAsync(Order order);
    Task<List<Order>> GetAllAsync();
}
