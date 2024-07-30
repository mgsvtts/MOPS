using Domain.Common;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.MerchItemAggregate.Entities;

public sealed class Image : Entity<ImageId>
{
    public MerchItemId MerchItemId { get; private set; }

    public string? Url { get; private set; }

    public bool IsMain { get; private set; }

    public Image(ImageId id, MerchItemId merchItemId, string? url = null, bool isMain = false) : base(id)
    {
        MerchItemId = merchItemId;
        Url = url;
        IsMain = isMain;
    }

    public Image NotMain()
    {
        IsMain = false;

        return this;
    }

    public Image WithItemId(MerchItemId itemId)
    {
        MerchItemId = itemId;

        return this;
    }
}