namespace Domain.MerchItemAggregate.Entities.ValueObjects.Types;
public sealed record TypeId
{
    public Guid Identity { get; init; }

    public TypeId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}
