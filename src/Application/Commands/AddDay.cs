using Application.Data;
using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class AddDay(IConsoleWrite consoleWrite, IDayStorage dayStorage) : IRequestHandler<AddDayCommand>
{
    private IConsoleWrite ConsoleWrite { get; } = consoleWrite;
    private IDayStorage DayStorage { get; } = dayStorage;

    public async Task Handle(AddDayCommand request, CancellationToken cancellationToken)
    {
        StringToDateOnly dateOnly = request.Values;
        if (dateOnly.OutOfDate())
        {
            ConsoleWrite.RedFontColorLine("A data esta no formato incorreto, ou não foi passada antes dos horários");
            return;
        }

        var timers = request.Values.Skip(1).ToArray();
        if (!timers.Any())
        {
            ConsoleWrite.RedFontColorLine("Não foi informado horário para a data específica");
            return;
        }

        foreach (var timerString in timers)
        {
            StringToTimeOnly timeOnly = timerString;
            if (timeOnly.OutOfTimer())
            {
                ConsoleWrite.RedFontColorLine("Não foi possível converte o horário {0}", timerString);
                continue;
            }
            var dataWithTimeOnly = dateOnly.Value.ToDateTime(timeOnly.Value);
            await DayStorage.Add(dataWithTimeOnly);
            ConsoleWrite.GreenFontColorLine(dataWithTimeOnly.ToString());
        }


        Console.WriteLine("Chegou até aqui parabéns vem mamar");
    }
}

public record AddDayCommand(string[] Values) : IRequest;

