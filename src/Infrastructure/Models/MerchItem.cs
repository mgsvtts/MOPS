using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("merch_items")]
public sealed class MerchItem : ITableEntity<Guid>
{
    public Guid Id { get; set; }

    [Column("type_id")]
    public Guid TypeId { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("self_price")]
    public decimal SelfPrice { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.Image.MerchItemId))]
    public List<Image> Images { get; set; } = [];

    [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.OrderItem.MerchItemId))]
    public List<OrderItem> OrderItems { get; set; } = [];

    [Association(ThisKey = nameof(TypeId), OtherKey = nameof(Models.Type.Id))]
    public Type Type { get; set; } = null!;
}