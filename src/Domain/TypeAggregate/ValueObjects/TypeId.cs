namespace Domain.TypeAggregate.ValueObjects;
public record struct TypeId
{
    public Guid Identity { get; init; }

    public TypeId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}