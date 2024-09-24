namespace Domain.Common.ValueObjects;

public record struct Price
{
    public decimal Value { get; init; }

    public Price(decimal value = 0)
    {
        if (value < 0)
        {
            throw new ArgumentException("Merch item price cannot be less than 0", nameof(value));
        }

        Value = value;
    }

    public static implicit operator decimal(Price price)
    {
        return price.Value;
    }

    public static implicit operator Price(decimal price)
    {
        return new Price(price);
    }
}