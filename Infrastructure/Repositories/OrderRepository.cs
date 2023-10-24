using Dapper;
using Domain.OrderAggregate.Repositories;
using Infrastructure.Models;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var orderQuery =  $@"INSERT INTO orders ({nameof(Order.id)},
                                                 {nameof(Order.created_at)},
                                                 {nameof(Order.payment_method)})
                             VALUES (@{nameof(Order.id)},
                                     @{nameof(Order.created_at)},
                                     @{nameof(Order.payment_method)})";

        var orderItemQuery = $@"INSERT INTO order_items ({nameof(OrderItem.id)}, 
                                                         {nameof(OrderItem.order_id)},
                                                         {nameof(OrderItem.merch_item_id)},
                                                         {nameof(OrderItem.amount)}, 
                                                         {nameof(OrderItem.price)})
                                VALUES (@{nameof(OrderItem.id)}, 
                                        @{nameof(OrderItem.order_id)},
                                        @{nameof(OrderItem.merch_item_id)},
                                        @{nameof(OrderItem.amount)}, 
                                        @{nameof(OrderItem.price)})";

        var dbOrder = _mapper.Map<Order>(order);
        var dbOrderItem = _mapper.Map<IEnumerable<OrderItem>>(order.Items);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(orderQuery, dbOrder);
        await connection.ExecuteAsync(orderItemQuery, dbOrderItem);
    }
}
