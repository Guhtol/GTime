namespace Application.SeedWork;
public readonly struct CommandName
{
    private readonly string? _value;

    private readonly string _options;

    private CommandName(string[] args)
    {
        var options = new List<string> { "ad", "ld", "cf", "rd", "rh", "tm","h","adm" };
        _options = string.Join(",", options);
        _value = args
                  .Where(x => options.Contains(x))
                  .FirstOrDefault();
    }

    public readonly bool NotFound() => string.IsNullOrEmpty(_value);

    public readonly string GetAll() => _options;

    public static implicit operator CommandName(string[] args) => new(args);

    public override readonly string ToString() => _value ?? string.Empty;
}
