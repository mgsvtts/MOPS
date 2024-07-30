namespace Domain.MerchItemAggregate.ValueObjects;

public record struct MerchItemId
{
    public Guid Identity { get; init; }
    public MerchItemId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}