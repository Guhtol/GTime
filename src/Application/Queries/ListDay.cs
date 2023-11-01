using Application.Data;
using Application.Domain;
using Application.SeedWork;
using MediatR;

namespace Application.Queries;
public class ListDay : IRequestHandler<ListDayCommand>
{
    private IDayStorage DayStorage { get; }
    private IConsoleWrite ConsoleWrite { get; }
    public ListDay(IDayStorage dayStorage, IConsoleWrite consoleWrite)
    {
        DayStorage = dayStorage;
        ConsoleWrite = consoleWrite;

    }

    public async Task Handle(ListDayCommand request, CancellationToken cancellationToken)
    {
        StringToDateOnly dateOnly = request.Date;
        var queryResult = await DayStorage.GetAll(dateOnly.Value).ConfigureAwait(false);

        if (!queryResult.Any())
        {
            ConsoleWrite.RedFontColorLine("Não foi possível encontrar nenhum dia para o dia pesquisado {0}", request.Date);
            return;
        }

        var daies = Day.ConvertListDateTimeToDay(queryResult);
        _ = new DayHoursWriter(ConsoleWrite, daies);
    }

}

public record ListDayCommand(string Date) : IRequest;


