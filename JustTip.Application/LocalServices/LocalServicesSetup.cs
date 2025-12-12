using JustTip.Application.LocalServices.Abs;
using JustTip.Application.LocalServices.AppServices;
using JustTip.Application.LocalServices.Imps;
using Microsoft.Extensions.DependencyInjection;

namespace JustTip.Application.LocalServices;
public static class LocalServicesSetup
{

    internal static IServiceCollection AddLocalServices(this IServiceCollection services)
    {

        return services
            .AddScoped<IRosterUtils, RosterUtils>()
            .AddScoped<ITipCalculatorService, TipCalculatorService>();

    }

}//Cls
