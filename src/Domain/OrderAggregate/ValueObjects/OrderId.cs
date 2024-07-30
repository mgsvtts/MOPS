namespace Domain.OrderAggregate.ValueObjects;

public record struct OrderId
{
    public Guid Identity { get; init; }
    public OrderId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}