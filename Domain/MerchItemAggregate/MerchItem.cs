using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities.ValueObjects.Types;
using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.MerchItemAggregate;

public class MerchItem : AggregateRoot<MerchItemId>
{
    public TypeId TypeId { get; private set; }

    public Name Name { get; private set; }

    public Description? Description { get; private set; }

    public MerchItemPrice Price { get; private set; }

    public MerchItemPrice SelfPrice { get; private set; }

    public MerchItemAmount AmountLeft { get; private set; }

    public MerchItem(MerchItemId id,
                     TypeId typeId,
                     Name? name = null,
                     Description? description = null,
                     MerchItemPrice? price = null,
                     MerchItemPrice? selfPrice = null,
                     MerchItemAmount? amountLeft = null) : base(id)
    {
        Id = id;
        TypeId = typeId;
        Name = name ?? new Name("Без названия");
        Description = description;
        Price = price ?? new MerchItemPrice();
        SelfPrice = selfPrice ?? new MerchItemPrice();
        AmountLeft = amountLeft ?? new MerchItemAmount();
    }
}
