namespace Domain.MerchItemAggregate.ValueObjects;

public record struct MerchItemAmount
{
    public int Value { get; init; }

    public MerchItemAmount(int value = 0)
    {
        if (value < 0)
        {
            throw new ArgumentException("Merch item amount cannot be less than 0", nameof(value));
        }

        Value = value;
    }
}