namespace Domain.OrderAggregate.ValueObjects;

public sealed record OrderId
{
    public Guid Identity { get; init; }
    public OrderId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}
