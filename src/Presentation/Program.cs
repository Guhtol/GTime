using Application.Commands;
using Application;
using Application.SeedWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();


var appsettings = new AppSettings();

configuration.GetSection("AppSettings").Bind(appsettings);

var serviceCollection = new ServiceCollection();

serviceCollection
    .AddApplicationConfig(appsettings)
    .AddInfrasctructureConfig(appsettings);

var serviceProvider = serviceCollection.BuildServiceProvider();

await RunProgram(serviceProvider, args);

static async Task RunProgram(IServiceProvider hostProvider, string[] args)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    IMediator mediator = provider.GetRequiredService<IMediator>();
    IConsoleWrite consoleWrite = provider.GetRequiredService<IConsoleWrite>();

    consoleWrite.WhiteFontColorLine("Olá seja bem vindo ao GTime");
    do
    {
        consoleWrite.WhiteFontColorLine("Digite um comando para iniciar");
        var result = Console.ReadLine();
      
        if (string.IsNullOrEmpty(result))
        {
            continue;
        }
        await mediator.Send(new RunApplicationCommand(result.Split(" ")));
        consoleWrite.WhiteFontColorLine("\n Digite qualquer tecla para continuar ou esc para sair");

    } while (ConsoleKey.Escape != Console.ReadKey().Key);

}