using Domain.Common;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public PaymentMethod PaymentMethod { get; set; }

    public DateTime CreatedAt { get; set; }

    public Order(OrderId id,
                 IEnumerable<OrderItem> items,
                 PaymentMethod paymentMethod,
                 DateTime? createdAt = null) : base(id)
    {
        _items = items.ToList();

        PaymentMethod = paymentMethod;
        CreatedAt = createdAt ?? DateTime.Now;
    }

    public void SetOrderIdToOrderItems()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            _items[i] = _items[i] with { OrderId = Id };
        }
    }
}
