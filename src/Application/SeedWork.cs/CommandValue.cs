namespace Application.SeedWork;
public readonly struct CommandValue
{
    private readonly string[] _value;

    private CommandValue(string[] args)
    {
        if (args.Length <= 1)
        {
            _value = args;
            return;
        }

        _value = args.Skip(1).ToArray();
    }

    public static implicit operator CommandValue(string[] args) => new(args);

    public string[] GetValue => _value;

    public string GetFirstValue => _value.FirstOrDefault() ?? string.Empty;

    public override readonly string ToString() => string.Join(", ", _value);
}
