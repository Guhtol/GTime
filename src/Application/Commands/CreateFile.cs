using Application.Data;
using Application.Domain;
using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class CreateFile(IConsoleWrite consoleWrite, IDayStorage dayStorage, AppSettings appSettings) : IRequestHandler<CreateFileCommand>
{
    private IConsoleWrite ConsoleWrite { get; } = consoleWrite;
    private IDayStorage DayStorage { get; } = dayStorage;
    private readonly string pathFile = appSettings.PathFile;


    public async Task Handle(CreateFileCommand request, CancellationToken cancellationToken)
    {
        var parsed = int.TryParse(request.Month, out int month);
        FirstDateOfMonth firstDate = month;
        LastDateOfMonth lastDate = month;

        if (!parsed || firstDate.OutOfMonth() || lastDate.OutOfMonth())
        {
            ConsoleWrite.RedFontColorLine("Erro ao tentar converter o mês para número {0}", request.Month);
            return;
        }

        IEnumerable<DateTime> queryResult = await DayStorage
                                                .GetAllDaiesByMonth(firstDate, lastDate)
                                                .ConfigureAwait(false);

        if (!queryResult.Any())
        {
            ConsoleWrite.RedFontColor("Não foi encontrado registro para o mês {0}", request.Month);
        }

        var daies = Day.ConvertListDateTimeToDay(queryResult);
        var nameDocument =$"Gtime-{DateTime.Now:dd-MM-yyyy}.csv";
        NewFileStream newFileStream = new(@$"{pathFile}{nameDocument}");
        await newFileStream.CreateAsync(daies);
        ConsoleWrite.GreenFontColor(@"Arquivo criado com sucesso {0}{1}",pathFile,nameDocument);
    }
}

public record CreateFileCommand(string Month) : IRequest;