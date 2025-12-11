using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Application.Domain.Entities.Tips;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JustTip.Infrastructure.Persistence.Repos.Setup;
internal static  class SetupRepos
{
    internal static IServiceCollection AddRepos(this IServiceCollection services)
    {
        services.TryAddScoped<IEmployeeRepo, EmployeeRepo>();
        services.TryAddScoped<ITipRepo, TipRepo>();
        services.TryAddScoped<IJtOutboxMessageRepo, JtOutboxMessageRepo>();

        services.TryAddScoped<IJtUnitOfWork, JtUnitOfWork>();

        return services;

    }
}
