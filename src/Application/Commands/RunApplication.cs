using Application.SeedWork;
using MediatR;

namespace Application.Commands;
public class RunApplication(IConsoleWrite consoleWrite, IMediator mediator) : IRequestHandler<RunApplicationCommand>
{
    private IConsoleWrite ConsoleWrite { get; } = consoleWrite;
    private IMediator Mediator { get; } = mediator;

    public async Task Handle(RunApplicationCommand request, CancellationToken cancellationToken)
    {
        if (request.Args is null || request.Args.Length == 0)
        {
            ConsoleWrite.RedFontColorLine("É necessário passar os parametros para executar o aplicativo");
            return;
        }

        CommandName commandName = request.Args;
        CommandValue commandValue = request.Args;

        if (commandName.NotFound())
        {
            ConsoleWrite.RedFontColorLine("Os comandos válidos são {0}", commandName.GetAll());
            return;
        }

        CommandStrategy commandStrategy = (commandName.ToString(), commandValue);
        await Mediator.Send(commandStrategy.GetCommand(), cancellationToken);
    }
}

public record RunApplicationCommand(string[] Args) : IRequest;
