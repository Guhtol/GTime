using Application.Data;
using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class RemoveDay : IRequestHandler<RemoveDayCommand>
{
    private IConsoleWrite ConsoleWrite { get; }
    private IDayStorage DayStorage { get; }

    public RemoveDay(IConsoleWrite consoleWrite, IDayStorage dayStorage)
    {
        ConsoleWrite = consoleWrite;
        DayStorage = dayStorage;

    }


    public async Task Handle(RemoveDayCommand request, CancellationToken cancellationToken)
    {
        StringToDateOnly dateOnly = request.Date;
        if (dateOnly.OutOfDate())
        {
            ConsoleWrite.RedFontColorLine("Data {0} em formato inválido", request.Date);
            return;
        }

        ExecuteSqlResult executeSqlResult = await DayStorage.RemoveDay(dateOnly.Value).ConfigureAwait(false);
        
        executeSqlResult.ValidateAndWriteResult(ConsoleWrite,
                            $"Data {request.Date} removida com sucesso",
                            $"Data {request.Date} não encontrad,a operação não realizada");
    }

}

public record RemoveDayCommand(string Date) : IRequest;
