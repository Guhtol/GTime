using System.Text;

namespace Application.Domain;
public class NewFileStream
{
    private string Path { get; }

    public NewFileStream(string path)
    {
        Path = path;
    }

    public async Task CreateAsync(IEnumerable<Day> dais)
    {
        using StreamWriter sw = new(File.Create(Path), Encoding.UTF8);
        await sw.WriteAsync(";Lançamento Gtime;");
        await sw.WriteLineAsync("\n Dia;Entrada;Saída;Total");
        TimeSpan totalMonth = new();
        List<string> listTotal = new();

        foreach (var item in dais)
        {
            totalMonth += await WriteFileAndReturnTotalAsync(sw, item, listTotal);
        }
        
        //TODO trocar para async
        sw.WriteLine("\n Dia;Total Horário;\n");
        listTotal.ForEach(sw.WriteLine);
        sw.WriteLine("Total;{0:00}:{1:00};", (int)totalMonth.TotalHours, totalMonth.Minutes);
    }

    private async Task<TimeSpan> WriteFileAndReturnTotalAsync(StreamWriter sw, Day day, List<string> listTotalDay)
    {

        var last = new TimeOnly();
        var totalDay = new TimeSpan();
        var totalLine = new TimeSpan();

        if (day.Times.Count % 2 != 0)
        {
            last = day.Times.LastOrDefault();
            day.Times.Remove(last);
        }

        for (int i = 0; i < day.Times.Count; i = i + 2)
        {
            TimeOnly item = day.Times[i];
            var nextIndex = i + 1;
            TimeOnly nextItem = day.Times[nextIndex];
            var timeTotal = nextItem - item;

            totalDay = totalDay.Add(timeTotal);
            totalLine = timeTotal;
            await sw.WriteLineAsync($"{day.Date};{item};{nextItem};{(int)totalLine.TotalHours:00}:{totalLine.Minutes:00}");
        }
        if (last != default)
            await sw.WriteLineAsync($"{day.Date};{last}");

        listTotalDay.Add($"{day.Date};{totalDay}");

        return totalDay;
    }

}
