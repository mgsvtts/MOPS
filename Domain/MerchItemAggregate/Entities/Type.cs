using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities.ValueObjects.Types;

namespace Domain.MerchItemAggregate.Entities;

public class Type : Entity<TypeId>
{
    public Name Name { get; private set; }

    public Type(TypeId id, Name name) : base(id)
    {
        Name = name;
    }
}
