namespace Domain.Common.ValueObjects;

public record struct Name
{
    private const int _maxLength = 50;

    public string Value { get; init; }

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Name cannot be null", nameof(value));
        }

        if (value.Length > _maxLength)
        {
            throw new ArgumentException($"Name too long, max length is {_maxLength}");
        }

        Value = value;
    }
}
