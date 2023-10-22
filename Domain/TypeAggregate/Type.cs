using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
namespace Domain.TypeAggregate;

public class Type : AggregateRoot<TypeId>
{
    public Name Name { get; private set; }

    public Type(TypeId id, Name name) : base(id)
    {
        Name = name;
    }
}
