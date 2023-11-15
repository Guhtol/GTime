using Application.Data;
using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class RemoveDay(IConsoleWrite consoleWrite, IDayStorage dayStorage) : IRequestHandler<RemoveDayCommand>
{
    private IConsoleWrite ConsoleWrite { get; } = consoleWrite;
    private IDayStorage DayStorage { get; } = dayStorage;


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
