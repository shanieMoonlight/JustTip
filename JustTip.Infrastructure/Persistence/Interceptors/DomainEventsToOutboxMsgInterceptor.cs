using JustTip.Application.Domain.Entities.Common.Events;
using JustTip.Application.Domain.Entities.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JustTip.Infrastructure.Persistence.Interceptors;
public sealed class DomainEventsToOutboxMsgInterceptor : SaveChangesInterceptor
{

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DbContext? dbCtx = eventData.Context;
        if (dbCtx == null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);


        var events = dbCtx.ChangeTracker
                .Entries<IJtDomainEventEntity>()
                .Select(e => e.Entity)
                .SelectMany(bk =>
                {
                    var dEvs = bk.GetDomainEvents();
                    bk.ClearDomainEvents();
                    return dEvs;
                })
                .Select(ev => JtOutboxMessage.Create(ev))
                .ToList();

        await dbCtx.Set<JtOutboxMessage>().AddRangeAsync(events, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);

    }



}//Cls
