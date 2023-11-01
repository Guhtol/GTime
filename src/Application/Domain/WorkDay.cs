namespace Application.Domain;
public class WorkDay
{
    public int HoursByDayNumber { get; set; }
    public int HoursByWeeksNumber { get; set; }
    public float WeeksNumber { get; set; }
    public TimeOnly HoursByDay { get => new(HoursByDayNumber, minute: 0, second: 0); }
    public TimeSpan HoursByWeeks { get => new(HoursByWeeksNumber, minutes: 0, seconds: 0); }
    public int TotalMonth => Convert.ToInt32(HoursByWeeksNumber * WeeksNumber);

    public WorkDay()
    {        
    }

    public WorkDay(int hoursByDayNumber, int hoursByWeeksNumber, float weeksNumber)
    {
        HoursByDayNumber = hoursByDayNumber;
        HoursByWeeksNumber = hoursByWeeksNumber;
        WeeksNumber = weeksNumber;
    }

    public int CalculateValueByHoursOfDay(int value) => value * HoursByDayNumber;
    public static string GetTotalMonthWithCurrentLog(int totalMonth, TimeSpan log)
    {
        var total = new TimeSpan(totalMonth, 0, 0);
        var result = log.Subtract(total);

        return $"{result.TotalHours:00}:{result.TotalMinutes:00}";
    }
}
