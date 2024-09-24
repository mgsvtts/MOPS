using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.OrderAggregate.ValueObjects;
public record struct OrderItem
{
    public MerchItemId ItemId { get; init; }
    public MerchItemAmount Amount { get; init; }
    public Price Price { get; init; }
    public Price SelfPrice { get; init; }

    public OrderItem(MerchItemId itemId, MerchItemAmount amount)
    {
        ItemId = itemId;
        Amount = amount;
        Price = new Price();
        SelfPrice = new Price();
    }

    public OrderItem(MerchItemId itemId, MerchItemAmount amount, Price price, Price selfPrice)
    {
        ItemId = itemId;
        Amount = amount;
        Price = price;
        SelfPrice = selfPrice;
    }
}