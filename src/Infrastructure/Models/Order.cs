﻿using Domain.OrderAggregate.ValueObjects;
using LinqToDB.Mapping;

namespace Infrastructure.Models;

[Table("orders")]
public sealed class Order : ITableEntity<Guid>
{
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("payment_method")]
    public PaymentMethod PaymentMethod { get; set; }

    [Association(ThisKey = nameof(Id), OtherKey = nameof(OrderItem.OrderId))]
    public List<OrderItem> OrderItems { get; set; } = [];
}