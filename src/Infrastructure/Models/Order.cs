using Domain.OrderAggregate.ValueObjects;
using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("orders")]
public sealed class Order
{
    [PrimaryKey]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("payment_method")]
    public PaymentMethod PaymentMethod { get; set; }
}