namespace Domain.MerchItemAggregate.ValueObjects;

public sealed record MerchItemId
{
    public Guid Identity { get; init; }
    public MerchItemId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}
