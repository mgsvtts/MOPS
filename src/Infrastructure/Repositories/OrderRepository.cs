using Domain.OrderAggregate.Repositories;
using Domain.OrderAggregate.ValueObjects;
using Infrastructure.Common;
using LinqToDB;
using LinqToDB.Data;
using Mapster;
using Order = Domain.OrderAggregate.Order;

namespace Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken token)
    {
        var dbOrder = order.Adapt<Order>();
        var dbOrderItems = order.Items.Adapt<List<Models.OrderItem>>();

        using var db = new DbConnection();

        try
        {
            await db.BeginTransactionAsync(token);

            await db.InsertOrReplaceAsync(dbOrder, token: token);
            await db.BulkCopyAsync(dbOrderItems, token);

            await db.CommitTransactionAsync(token);
        }
        catch
        {
            await db.RollbackTransactionAsync(token);

            throw;
        }
    }

    public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken token)
    {
        using var db = new DbConnection();

        var item = await db.Orders.FirstOrDefaultAsync(x => x.Id == id.Identity);

        return item.Adapt<Order>();
    }

    public async Task DeleteAsync(Order order, CancellationToken token)
    {
        using var db = new DbConnection();

        await db.Orders
            .Where(x => x.Id == order.Id.Identity)
            .DeleteAsync(token);
    }
}