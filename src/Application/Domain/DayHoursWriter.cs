using Application.SeedWork;

namespace Application.Domain;
public class DayHoursWriter
{
    private IConsoleWrite ConsoleWrite { get; }
    public TimeSpan Total { get; } = new TimeSpan();
    public DayHoursWriter(IConsoleWrite consoleWrite, IEnumerable<Day> daies, bool shortVision = false)
    {
        ConsoleWrite = consoleWrite;
        Total = GetTotalDayAndWriteTotal(daies, shortVision);
    }

    public void WriteTotalFooter(int expectedMonth, int expectedTotalMonth)
    {
        ConsoleWrite.WhiteFontColor("\n Total do Mês: ");
        ConsoleWrite.GreenFontColor("{0:00}:{1:00}", Total.TotalHours, Total.Minutes);
        ConsoleWrite.WhiteFontColor("\n Esperado do Mês: ");
        ConsoleWrite.GreenFontColor("{0:00}:00", expectedMonth);
        ConsoleWrite.WhiteFontColor("\n Total Esperado do Mês:");
        ConsoleWrite.GreenFontColorLine("{0:00}:00", expectedTotalMonth);
    }
    private TimeSpan GetTotalDayAndWriteTotal(IEnumerable<Day> daies, bool shortVision)
    {
        if (!daies.Any()) return default;
        TimeSpan result = new();

        foreach (var day in daies)
        {
            WriteTitleDay(day.Date);
            TimeSpan totalDay = new();

            var lastTimer = new TimeOnly();
            if (ListIsOdd(day.Times.Count))
            {
                lastTimer = day.Times.LastOrDefault();
                day.Times.Remove(lastTimer);
            }

            for (int i = 0; i < day.Times.Count; i += 2)
            {
                (TimeOnly item, TimeOnly nextItem) = GetCurrentAndNextItem(i, day);
                var timeTotal = nextItem - item;

                totalDay = totalDay.Add(timeTotal);
                WriteBodyDay(item, nextItem);

                if (shortVision)
                {
                    ConsoleWrite.WhiteFontColor("\n");
                    continue;
                }

                ConsoleWrite.WhiteFontColor(" Total:");
                ConsoleWrite.GreenFontColor(" {0} \n", timeTotal);
            }
            if (lastTimer != default)
                ConsoleWrite.WhiteFontColor(" Entrada: {0}", lastTimer);

            WriteFooterDay(totalDay);
            result = result.Add(totalDay);
        }

        return result;
    }
    private void WriteTitleDay(DateOnly date)
    {
        ConsoleWrite.WhiteFontColor("\n Data de Lançamento:");
        ConsoleWrite.GreenFontColor(" {0} \n", date);
    }

    private static (TimeOnly current, TimeOnly next) GetCurrentAndNextItem(int index, Day day)
    {
        TimeOnly current = day.Times[index];

        var nextIndex = index + 1;
        TimeOnly next = day.Times[nextIndex];
        return (current, next);
    }
    private void WriteBodyDay(TimeOnly first, TimeOnly second)
    {
        ConsoleWrite.WhiteFontColor(" Entrada: ", first);
        ConsoleWrite.BlueFontColor("{0}", first);
        ConsoleWrite.WhiteFontColor(" Saída: ", second);
        ConsoleWrite.BlueFontColor("{0}", second);
    }

    private void WriteFooterDay(TimeSpan totalDay)
    {
        ConsoleWrite.WhiteFontColor(" Total Dia:");
        ConsoleWrite.GreenFontColor(" {0} \n", totalDay);
    }

    private static bool ListIsOdd(int quantity) => (quantity % 2) != 0;
}
