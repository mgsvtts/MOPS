namespace Domain.MerchItemAggregate.Entities.ValueObjects.Images;

public sealed record ImageId
{
    public Guid Identity { get; init; }
    public ImageId(Guid? identity = null)
    {
        Identity = identity ?? Guid.NewGuid();
    }
}
