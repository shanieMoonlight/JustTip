using FluentValidation;
using JustTip.Application.Mediatr.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace JustTip.Application.Mediatr;
public static class MediatrSetup
{

    internal static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        var assembly = typeof(JtApplicationeAssemblyReference).Assembly;


        services.AddValidatorsFromAssembly(assembly);


        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            //config.AddOpenBehaviour(typeof(LoggingPipelineBehaviour<,>));
            config.AddOpenBehavior(typeof(JtUnitOfWorkPipelineBehaviour<,>));
            config.AddOpenBehavior(typeof(JtValidationPipelineBehaviour<,>));
        });

        return services;

    }

}//Cls
