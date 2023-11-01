using System.Data;
using Application;
using Application.Data;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;

namespace Microsoft.Extensions.DependencyInjection;
public static class Extension
{
    public static IServiceCollection AddInfrasctructureConfig(this IServiceCollection services, AppSettings appSetting)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fileNameFull = Path.Combine(baseDirectory, appSetting.ConnectionString);
 
        services
            .AddScoped<IDbConnection, SqliteConnection>(service =>
                new SqliteConnection($"Data Source={fileNameFull}"));

        services
            .AddScoped<IDayStorage, DayStorage>();
            
        return services;    
    }

}
