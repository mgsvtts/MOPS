using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("order_items")]
public sealed class OrderItem
{
    [PrimaryKey]
    [Column("id")]
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
}