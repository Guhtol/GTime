namespace Application.SeedWork;
public interface IConsoleWrite
{
    void WhiteFontColor(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.White;
        ConsoleWrite(text, values);
    }

    void WhiteFontColorLine(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.White;
        ConsoleWriteLine(text, values);
    }

    void BlueFontColor(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        ConsoleWrite(text, values);
    }

    void BlueFontColorLine(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        ConsoleWriteLine(text, values);
    }
    void GreenFontColor(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(text, values);
    }
    void GreenFontColorLine(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        ConsoleWriteLine(text, values);
    }

    public void RedFontColor(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        ConsoleWrite(text, values);
    }
    void RedFontColorLine(string text, params object[] values)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        ConsoleWriteLine(text, values);
    }
    void ConsoleWrite(string text, params object[] values)
    {
        Console.Write(text, values);
    }

    void ConsoleWriteLine(string text, params object[] values)
    {
        Console.WriteLine(text, values);
    }
}
