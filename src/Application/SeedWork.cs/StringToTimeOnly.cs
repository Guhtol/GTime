namespace Application.SeedWork;
public readonly struct StringToTimeOnly
{
    private readonly TimeOnly _value;

    private StringToTimeOnly(string value)
    {
         _ = TimeOnly.TryParse(value, out TimeOnly timeOnly);
         _value = timeOnly;
    }

    public readonly TimeOnly Value => _value;
    public readonly bool OutOfTimer() => _value == TimeOnly.MinValue;

    public static implicit operator StringToTimeOnly(string values) => new(values);
    public override readonly string ToString() => _value.ToString();
}
