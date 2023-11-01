using System.Net.NetworkInformation;

namespace Application.SeedWork;
public readonly struct ExecuteSqlResult
{
    private readonly int _value;

    private ExecuteSqlResult(int value)
    {
        _value = value;
    }

    private bool Success() => _value > 0;
    public void ValidateAndWriteResult(IConsoleWrite consoleWrite, string success, string error)
    {
        if (Success())
        {
            consoleWrite.GreenFontColorLine(success);
            return;
        }

        consoleWrite.RedFontColorLine(error);
    }

    public static implicit operator ExecuteSqlResult(int value) => new(value);
}
