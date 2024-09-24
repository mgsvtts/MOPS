using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public PaymentMethod PaymentMethod { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Price Price => _items.Sum(x => x.Price);

    public Order(OrderId id,
                 IEnumerable<OrderItem>? items,
                 PaymentMethod paymentMethod,
                 DateTime? createdAt = null) : base(id)
    {
        if (items is not null)
        {
            _items = items.ToList();
        }

        PaymentMethod = paymentMethod;
        CreatedAt = createdAt ?? DateTime.Now;
    }

    public Order AddOrderItems(IEnumerable<OrderItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items), "Order items cannot be null");
        }

        _items.AddRange(items);

        return this;
    }

    public Order SetActualPriceToItems(IEnumerable<MerchItem> actualItems)
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var actualItem = actualItems.First(x => x.Id == _items[i].ItemId);
            _items[i] = _items[i] with
            {
                Price = actualItem.Price,
                SelfPrice = actualItem.SelfPrice
            };
        }

        return this;
    }
}