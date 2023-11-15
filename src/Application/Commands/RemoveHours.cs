using Application.Data;
using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class RemoveHours(IConsoleWrite consoleWrite, IDayStorage dayStorage) : IRequestHandler<RemoveHoursCommand>
{
    private IConsoleWrite ConsoleWrite { get; } = consoleWrite;
    private IDayStorage DayStorage { get; } = dayStorage;


    public async Task Handle(RemoveHoursCommand request, CancellationToken cancellationToken)
    {
        if (!DateTime.TryParse(string.Join(" ",request.Date), out DateTime dateTime))
        {
            ConsoleWrite.RedFontColor("Data em formato inválido");
            return;
        }

        ExecuteSqlResult executeSqlResult = await DayStorage.RemoveHours(dateTime).ConfigureAwait(false);
        executeSqlResult.ValidateAndWriteResult(ConsoleWrite,
                            $"Data {dateTime} removida com sucesso",
                            $"Não foi possível remover a data {dateTime}");
    }

}

public record RemoveHoursCommand(string[] Date) : IRequest;