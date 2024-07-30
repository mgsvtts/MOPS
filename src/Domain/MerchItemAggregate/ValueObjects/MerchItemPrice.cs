namespace Domain.MerchItemAggregate.ValueObjects;

public record struct MerchItemPrice
{
    public decimal Value { get; init; }

    public MerchItemPrice(decimal value = 0)
    {
        if (value < 0)
        {
            throw new ArgumentException("Merch item price cannot be less than 0", nameof(value));
        }

        Value = value;
    }
}