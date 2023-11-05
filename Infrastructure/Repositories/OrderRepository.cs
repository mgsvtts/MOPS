using Dapper;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Repositories;
using Domain.OrderAggregate.ValueObjects;
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

    public async Task AddAsync(Order order)
    {
        var orderQuery = Queries.Order.AddOrder();
        var orderItemQuery = Queries.Order.AddOrderItems();

        var dbOrder = _mapper.Map<orders>(order);
        var dbOrderItems = _mapper.Map<List<order_items>>(order.Items);
        dbOrderItems.ForEach(x => x.order_id = dbOrder.id);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(orderQuery, dbOrder);
        await connection.ExecuteAsync(orderItemQuery, dbOrderItems);
    }

    public async Task<Order?> GetByIdAsync(OrderId id)
    {
        var query = Queries.Order.GetById();

        using var connection = _db.CreateConnection();

        var order = await connection.QueryFirstOrDefaultAsync<orders>(query, new { id = id.Identity.ToString() });

        return _mapper.Map<Order>(order);
    }

    public async Task DeleteAsync(Order order)
    {
        var query = Queries.Order.Delete();

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new { id = order.Id.Identity.ToString() });
    }
}
