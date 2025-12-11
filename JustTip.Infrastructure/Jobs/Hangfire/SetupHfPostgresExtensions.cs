using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace JustTip.Infrastructure.Jobs.Hangfire;

public static class SetupHfInMemoryExtensions
{

    /// <summary>
    /// Setup Hangfire and it's helpers
    /// Setup MyId first because you will need it's TokenIssuer to complete the HF Setup
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddHangfireHelper_Postgres(
        this IServiceCollection services,
        string connectionString,
        string? serverName = "JtHangfireServer")
    {
        var jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };


        var postgresOptions = BuildDefaultPostgresOptions();

        services.AddHangfire(x => x
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            //.UseSerializerSettings(jsonSettings)
            .UsePostgreSqlStorage(configure =>
            {
                configure.UseNpgsqlConnection(connectionString);
            },
            postgresOptions)
        );

        // Add the processing server as IHostedService
        return services.AddHangfireServer(config => config.ServerName = serverName);
    }

    //- - - - - - - - - - - - - - - - //

    private static PostgreSqlStorageOptions BuildDefaultPostgresOptions() => new()
    {
        QueuePollInterval = TimeSpan.FromSeconds(15),
        JobExpirationCheckInterval = TimeSpan.FromDays(1),
        CountersAggregateInterval = TimeSpan.FromMinutes(5),
        PrepareSchemaIfNecessary = true,
        TransactionSynchronisationTimeout = TimeSpan.FromSeconds(30),
        UseNativeDatabaseTransactions = true
    };

}//Cls