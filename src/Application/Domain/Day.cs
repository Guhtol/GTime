namespace Application.Domain;
public class Day
{
    public Day()
    {
    }
    public Day(DateTime dateTime)
    {
        Date = DateOnly.FromDateTime(dateTime);
        Times.Add(TimeOnly.FromDateTime(dateTime));
    }

    public DateOnly Date { get; private set; }
    public List<TimeOnly> Times { get; private set; } = new List<TimeOnly>();

    public static List<Day> ConvertListDateTimeToDay(IEnumerable<DateTime> dateTimes)
    {
        var daies = new List<Day>();
        var lastDay = new Day();

        foreach (var item in dateTimes)
        {
            if (lastDay.Date == DateOnly.FromDateTime(item))
            {
                lastDay.Times.Add(TimeOnly.FromDateTime(item));
                continue;
            }

            lastDay = new Day(item);
            daies.Add(lastDay);
        }
        return daies;
    }
}
