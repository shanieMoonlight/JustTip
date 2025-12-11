using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace JustTip.Presentation;
public static class PresentationSetup
{

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = JtPresentationAssemblyReference.Assembly;
        services.AddControllers()
            .PartManager.ApplicationParts.Add(new AssemblyPart(assembly));

        return services;
    }


}//Cls
