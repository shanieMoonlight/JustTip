using Hangfire;
using Hangfire.InMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace JustTip.Infrastructure.Jobs.Hangfire;

public static class SetupPostgresExtensions
{

    /// <summary>
    /// Setup Hangfire and it's helpers
    /// Setup MyId first because you will need it's TokenIssuer to complete the HF Setup
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddHangfireHelper_InMemory(
        this IServiceCollection services,
        string connectionString,
        string? serverName = "JtHangfireServer")
    {
        var jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };



        services.AddHangfire(x => x
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            //.UseSerializerSettings(jsonSettings)
            .UseInMemoryStorage()
        );

        // Add the processing server as IHostedService
        return services.AddHangfireServer(config => config.ServerName = serverName);
    }


}//Cls