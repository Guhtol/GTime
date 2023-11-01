using System.Reflection;
using Application;
using Application.SeedWork;

namespace Microsoft.Extensions.DependencyInjection;
public static class Extension
{
    public static IServiceCollection AddApplicationConfig(this IServiceCollection services, AppSettings appSetting) =>
        services
            .AddScoped<IConsoleWrite, ConsoleWrite>()
            .AddSingleton(appSetting)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

}
