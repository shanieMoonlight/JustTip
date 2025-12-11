using JustTip.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JustTip.Infrastructure.AppImps;
internal static class ServiceImpsSetup
{

    public static IServiceCollection AddApplicationImplementations(this IServiceCollection services)
    {
        services.TryAddScoped<IDbInitializer, DbInitializer>();

        return services;
    }


}//Cls
