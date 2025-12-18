using JustTip.Infrastructure.AppImps;
using JustTip.Infrastructure.Jobs;
using JustTip.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace JustTip.Infrastructure;
public static class InfrastructureSetup
{

    public static IServiceCollection AddJtInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services
            //.AddPersistenceEf_PG(connectionString)
            .AddPersistenceEf_SqlLite()
            .AddApplicationImplementations()
            .SetupJobServices(connectionString);

        return services;

    }

    
    //--------------------------------//

    public static IApplicationBuilder UseJtInfrastructure(
        this IApplicationBuilder app, string? jobDashBoardUrl = null)
    {

        app.UseJtJobs(jobDashBoardUrl);
        return app;

    }




}//Cls
