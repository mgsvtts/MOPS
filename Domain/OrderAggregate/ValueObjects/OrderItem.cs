using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.OrderAggregate.ValueObjects;
public record struct OrderItem
{
    public MerchItemId ItemId { get; init; }
    public MerchItemAmount Amount { get; init; }
    public MerchItemPrice Price { get; init; }
    public MerchItemPrice SelfPrice { get; init; }

    public OrderItem(MerchItemId itemId, MerchItemAmount amount)
    {
        ItemId = itemId;
        Amount = amount;
        Price = new MerchItemPrice();
        SelfPrice = new MerchItemPrice();
    }

    public OrderItem(MerchItemId itemId, MerchItemAmount amount, MerchItemPrice price, MerchItemPrice selfPrice)
    {
        ItemId = itemId;
        Amount = amount;
        Price = price;
        SelfPrice = selfPrice;
    }
}
