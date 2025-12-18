using JustTip.Infrastructure.Persistence.Interceptors;
using JustTip.Infrastructure.Persistence.Repos.Setup;
using JustTip.Infrastructure.Persistence.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static JustTip.Infrastructure.Persistence.Utils.JtPersistenceConfigConstants;


namespace JustTip.Infrastructure.Persistence;
internal static class PersistenceSetup_SqlLite
{

    internal static IServiceCollection AddPersistenceEf_SqlLite(
        this IServiceCollection services)
    {
        services.AddSingleton<DomainEventsToOutboxMsgInterceptor>();
        services.AddSingleton<DateTimeNormalizationSaveChangesInterceptor>();

        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        services.AddDbContext<JtDbContext>((sp, config) =>
        {
            var domainEventToOutboxMsgInterceptor = sp.GetService<DomainEventsToOutboxMsgInterceptor>();
            var dateTimeNormalizationSaveChangesInterceptor = sp.GetService<DateTimeNormalizationSaveChangesInterceptor>();

            List<IInterceptor> interceptors = [
                domainEventToOutboxMsgInterceptor!,
                    dateTimeNormalizationSaveChangesInterceptor!];

            // resolve host environment from the service provider
            var env = sp.GetService<IHostEnvironment>();

            config.UseSqlite(connection, sqliteOptions =>
            {
                sqliteOptions
                .MigrationsHistoryTable(JtPersistenceConfigConstants.Db.MIGRATIONS_HISTORY_TABLE, JtPersistenceConfigConstants.Db.SCHEMA);
            })
           .AddInterceptors(interceptors)
           .UseSnakeCaseNamingConvention();



            if (env?.IsDevelopment() ?? false)
            {
                config
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .LogTo(log => Debug.WriteLine(log), LogLevel.Information);
            }

        });



        services
            .AddHttpContextAccessor()
            .AddRepos();


        using var scope = services.BuildServiceProvider().CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<JtDbContext>();
        db.Database.EnsureCreated(); // for demos/tests, avoid migrations complexity


        return services;

    }


}//Cls
