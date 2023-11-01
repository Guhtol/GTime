using System.Diagnostics;
using Application.Commands;
using Application.Queries;
using MediatR;

namespace Application.SeedWork;
public readonly struct CommandStrategy
{
    private readonly string _commandName;
    private readonly CommandValue _commandValue;

    private CommandStrategy(string commandName, CommandValue commandValue)
    {
        _commandName = commandName;
        _commandValue = commandValue;
    }

    public static implicit operator CommandStrategy((string commandName, CommandValue commandValue) parameters) => new(parameters.commandName, parameters.commandValue);

    public readonly IRequest GetCommand() => _commandName switch

    {
        "ad" => new AddDayCommand(_commandValue.GetValue),
        "ld" => new ListDayCommand(_commandValue.GetFirstValue),
        "cf" => new CreateFileCommand(_commandValue.GetFirstValue),
        "rd" => new RemoveDayCommand(_commandValue.GetFirstValue),
        "rh" => new RemoveHoursCommand(_commandValue.GetValue),
        "tm" => new ListDayWorkOfMonthCommand(_commandValue.GetFirstValue),
        "adm" => new AddMonthCommand(_commandValue.GetFirstValue),
        _ => throw new NotImplementedException()

    };

}
