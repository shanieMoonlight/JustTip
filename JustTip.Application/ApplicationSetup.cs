using JustTip.Application.Mediatr;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JustTip.Application;
public static class ApplicationSetup
{

    public static IServiceCollection AddJtApplication(this IServiceCollection services)
    {
        services
            .AddMediatr()
            //.AddJtJobs()
            ;

        return services;
    }


    //--------------------------------//

    public static async Task<WebApplication> UseJtApplicationAsync(
        this WebApplication app)
    {

        //await app.Services.StartEssentialJobsAsync();
        return app;

    }



}//Cls
