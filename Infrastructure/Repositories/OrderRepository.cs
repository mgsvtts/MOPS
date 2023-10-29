﻿using Dapper;
using Domain.OrderAggregate.Repositories;
using Infrastructure.Models;
using MapsterMapper;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _db;
    private readonly IMapper _mapper;

    public OrderRepository(IMapper mapper, DbContext db)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task AddAsync(Domain.OrderAggregate.Order order)
    {
        var orderQuery = $@"INSERT INTO {nameof(orders)} ({nameof(orders.id)},
                                                 {nameof(orders.created_at)},
                                                 {nameof(orders.payment_method)})
                             VALUES (@{nameof(orders.id)},
                                     @{nameof(orders.created_at)},
                                     @{nameof(orders.payment_method)})";

        var orderItemQuery = $@"INSERT INTO {nameof(order_items)} ({nameof(order_items.id)},
                                                         {nameof(order_items.order_id)},
                                                         {nameof(order_items.merch_item_id)},
                                                         {nameof(order_items.amount)},
                                                         {nameof(order_items.price)},
                                                         {nameof(order_items.self_price)})
                                VALUES (@{nameof(order_items.id)},
                                        @{nameof(order_items.order_id)},
                                        @{nameof(order_items.merch_item_id)},
                                        @{nameof(order_items.amount)},
                                        @{nameof(order_items.price)},
                                        @{nameof(order_items.self_price)})";

        var dbOrder = _mapper.Map<orders>(order);
        var dbOrderItems = _mapper.Map<List<order_items>>(order.Items);
        dbOrderItems.ForEach(x => x.order_id = dbOrder.id);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(orderQuery, dbOrder);
        await connection.ExecuteAsync(orderItemQuery, dbOrderItems);
    }
}
