using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("order_items")]
public sealed class OrderItem : ITableEntity<Guid>
{
    public Guid Id { get; set; }

    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("merch_item_id")]
    public Guid MerchItemId { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("self_price")]
    public decimal SelfPrice { get; set; }

    [Association(ThisKey = nameof(MerchItemId), OtherKey = nameof(Models.MerchItem.Id))]
    public MerchItem MerchItem { get; set; } = null!;

    [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Models.Order.Id))]
    public Order Order { get; set; } = null!;
}