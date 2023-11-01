namespace Application.SeedWork;
public readonly struct StringToDateOnly
{
    private readonly DateOnly _value;

    private StringToDateOnly(string value)
    {        
        _ = DateOnly.TryParse(value, out DateOnly dateOnly);
        _value = dateOnly;
    }

    public readonly DateOnly Value => _value;

    public readonly bool OutOfDate() => _value == DateOnly.MinValue;

    public static implicit operator StringToDateOnly(string[] values) => new(values.FirstOrDefault() ?? string.Empty);
    public static implicit operator StringToDateOnly(string value) => new(value);
    public override readonly string ToString() => _value.ToString();
}
