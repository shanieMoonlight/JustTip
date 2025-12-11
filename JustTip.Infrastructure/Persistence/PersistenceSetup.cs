using EfHelpers.Interceptors;
using JustTip.Infrastructure.Persistence.Interceptors;
using JustTip.Infrastructure.Persistence.Repos.Setup;
using JustTip.Infrastructure.Persistence.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace JustTip.Infrastructure.Persistence;
internal static class PersistenceSetup
{

    internal static IServiceCollection AddPersistenceEf(
        this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<DomainEventsToOutboxMsgInterceptor>();
        services.AddSingleton<DateTimeNormalizationSaveChangesInterceptor>();

        services.AddDbContext<JtDbContext>((sp, config) =>
        {
            var domainEventToOutboxMsgInterceptor = sp.GetService<DomainEventsToOutboxMsgInterceptor>();
            var dateTimeNormalizationSaveChangesInterceptor = sp.GetService<DateTimeNormalizationSaveChangesInterceptor>();

            List<IInterceptor> interceptors = [
                domainEventToOutboxMsgInterceptor!,
                    dateTimeNormalizationSaveChangesInterceptor!];

            // resolve host environment from the service provider
            var env = sp.GetService<IHostEnvironment>();

            config.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions
                .EnableRetryOnFailure(3)
                .MigrationsHistoryTable(JtPersistenceConfigConstants.Db.MIGRATIONS_HISTORY_TABLE, JtPersistenceConfigConstants.Db.SCHEMA);
            })
           .AddInterceptors(interceptors)
           .UseSnakeCaseNamingConvention();



            if (env?.IsDevelopment() ?? false)
            {
                config
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true)
                    .LogTo(Console.WriteLine, LogLevel.Information);
            }

        });



        services
            .AddHttpContextAccessor()
            .AddRepos();

        return services;

    }


}//Cls
