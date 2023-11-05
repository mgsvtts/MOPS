﻿using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;

namespace Domain.MerchItemAggregate;

public class MerchItem : AggregateRoot<MerchItemId>
{
    private readonly List<Image> _images = new();

    public TypeId TypeId { get; private set; }

    public Name Name { get; private set; }

    public Description? Description { get; private set; }

    public MerchItemPrice Price { get; private set; }

    public MerchItemPrice SelfPrice { get; private set; }

    public MerchItemAmount AmountLeft { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Image> Images => _images.AsReadOnly();

    public MerchItem(MerchItemId id,
                     TypeId typeId,
                     Name? name = null,
                     Description? description = null,
                     MerchItemPrice? price = null,
                     MerchItemPrice? selfPrice = null,
                     MerchItemAmount? amountLeft = null,
                     DateTime? createdAt = null,
                     IEnumerable<Image>? images = null) : base(id)
    {
        Id = id;
        TypeId = typeId;
        Name = name ?? new Name("Без названия");
        Description = description;
        Price = price ?? new MerchItemPrice();
        SelfPrice = selfPrice ?? new MerchItemPrice();
        AmountLeft = amountLeft ?? new MerchItemAmount();
        CreatedAt = createdAt ?? DateTime.Now;
        _images = images is not null ? images.ToList() : new List<Image>();
    }

    public MerchItem SubtractAmount(MerchItemAmount amount)
    {
        if (amount.Value > AmountLeft.Value)
        {
            throw new InvalidOperationException("You cannot subtract more than original sum");
        }

        AmountLeft = new MerchItemAmount(AmountLeft.Value - amount.Value);

        return this;
    }

    public float GetBenefitPercent()
    {
        if (Price == SelfPrice)
        {
            return 0;
        }

        return (float)(Price.Value / SelfPrice.Value);
    }

    public MerchItem WithTypeId(TypeId typeId)
    {
        TypeId = typeId;

        return this;
    }

    public MerchItem WithName(Name name)
    {
        Name = name;

        return this;
    }

    public MerchItem WithDescription(Description? description)
    {
        Description = description;

        return this;
    }

    public MerchItem WithPrice(MerchItemPrice price)
    {
        Price = price;

        return this;
    }

    public MerchItem WithSelfPrice(MerchItemPrice selfPrice)
    {
        SelfPrice = selfPrice;

        return this;
    }

    public MerchItem WithAmount(MerchItemAmount amount)
    {
        AmountLeft = amount;

        return this;
    }
}
