using JustTip.Application.Jobs;
using JustTip.Application.LocalServices;
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
            .AddJtJobs()
            .AddLocalServices();

        return services;
    }


    //--------------------------------//

    public static async Task<IApplicationBuilder> UseJtApplicationAsync(
        this IApplicationBuilder app)
    {

        await app.ApplicationServices.StartRecurringJtJobs();
        return app;

    }



}//Cls
