using System.Linq;
using Application.Data;
using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class AddMonth : IRequestHandler<AddMonthCommand>
{
    private IConsoleWrite ConsoleWrite { get; }
    private List<TimeOnly> BaseHours { get; } = new List<TimeOnly>();
    private IDayStorage DayStorage { get; }

    public AddMonth(IConsoleWrite consoleWrite, AppSettings appSettings, IDayStorage dayStorage)
    {
        ConsoleWrite = consoleWrite;
        if (appSettings.BaseHours is null || !appSettings.BaseHours.Any())
            throw new ArgumentException("Não foi encontrado os parametros para a Base de horas");
        BaseHours = appSettings.BaseHours;
        DayStorage = dayStorage;

    }

    public async Task Handle(AddMonthCommand request, CancellationToken cancellationToken)
    {

        var parsed = int.TryParse(request.Month, out int month);
        LastDateOfMonth lastDate = month;

        if (!parsed || lastDate.OutOfMonth())
        {
            ConsoleWrite.RedFontColorLine("Erro ao tentar converter o mês para número {0}", request.Month);
            return;
        }
        var dates = Enumerable.Range(1, lastDate.Value.Day)
                             .Select(day => new DateOnly(lastDate.Value.Year, lastDate.Value.Month, day))
                             .Where(IsWorkDay)
                             .SelectMany(day => {
                                return BaseHours.Select(hour => day.ToDateTime(hour));
                             });

        foreach(var date in dates) {
            await DayStorage.Add(date).ConfigureAwait(false);
        }
        ConsoleWrite.GreenFontColorLine("Mês inserido com sucesso");
    }

    private static bool IsWorkDay(DateOnly date) => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
}

public record AddMonthCommand(string Month) : IRequest;
