using Application.Data;
using Application.Domain;
using Application.SeedWork;
using MediatR;

namespace Application.Queries;
public class ListDaysWorkOfMonth : IRequestHandler<ListDayWorkOfMonthCommand>
{
    private WorkDay WorkDay { get; }
    private IDayStorage DayStorage { get; }
    private IConsoleWrite ConsoleWrite { get; }
    public ListDaysWorkOfMonth(AppSettings appSettings, IDayStorage dayStorage, IConsoleWrite consoleWrite)
    {
        WorkDay = appSettings.WorkDay;
        DayStorage = dayStorage;
        ConsoleWrite = consoleWrite;
    }

    public async Task Handle(ListDayWorkOfMonthCommand request, CancellationToken cancellationToken)
    {
        var parsed = int.TryParse(request.Month, out int month);
        if (!parsed)
        {
            ConsoleWrite.RedFontColorLine("Não foi possível converte o mês {0}", request.Month);
            return;
        }
        
        FirstDateOfMonth firstDate = month;
        LastDateOfMonth lastDate = month;

        var queryResult = await DayStorage.GetAllDaiesByMonth(firstDate, lastDate).ConfigureAwait(false);
        var daies = Day.ConvertListDateTimeToDay(queryResult);
        var totalDays = new DayHoursWriter(ConsoleWrite, daies, shortVision: true);

        int expectedMonth = WorkDay.CalculateValueByHoursOfDay(daies.Count);
        int missingHours = Convert.ToInt32(expectedMonth - totalDays.Total.TotalHours);
        totalDays.WriteTotalFooter(expectedMonth, WorkDay.TotalMonth);

        if (missingHours > 0)
            ConsoleWrite.RedFontColor("\n Total Faltando: {0:00}:00", missingHours);
    }
}

public record ListDayWorkOfMonthCommand(string Month) : IRequest;
