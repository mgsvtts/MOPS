using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;

namespace Domain.MerchItemAggregate.Entities;

public class Image : Entity<ImageId>
{
    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public Image(ImageId id, Name name, Description description) : base(id)
    {
        Name = name;
        Description = description;
    }
}
