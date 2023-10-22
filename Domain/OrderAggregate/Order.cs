using Domain.Common;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate;

public class Order : AggregateRoot<OrderId>
{
    public MerchItemAmount Amount { get; private set; }

    public MerchItemId ItemId { get; private set; }

    public MerchItemPrice Price { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public Order(OrderId id,
                 MerchItemAmount amount,
                 MerchItemId itemId,
                 MerchItemPrice price,
                 PaymentMethod paymentMethod) : base(id)
    {
        Amount = amount;
        ItemId = itemId;
        Price = price;
        PaymentMethod = paymentMethod;
    }
}
