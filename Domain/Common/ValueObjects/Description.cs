namespace Domain.Common.ValueObjects;

public record struct Description
{
    private const int _maxLength = 300;

    public string Value { get; init; }

    public Description(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Description cannot be null", nameof(value));
        }

        if (value.Length > _maxLength)
        {
            throw new ArgumentException($"Description too long, max length is {_maxLength}");
        }

        Value = value;
    }
}
