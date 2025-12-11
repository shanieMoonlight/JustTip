using JustTip.Application.Domain.Entities.Common.Events;
using MassTransit;

namespace JustTip.Application.Domain.Entities.Common;
public abstract class JtBaseDomainEntity: IJtDomainEventEntity
{
    public Guid Id { get; init; }
    public DateTime? CreatedDate { get; private set; } = DateTime.UtcNow;
    public DateTime? LastModifiedDate { get; private set; }

    //--------------------------// 

    protected JtBaseDomainEntity()
    {
        Id = NewId.NextSequentialGuid();
    }

    public JtBaseDomainEntity SetModified()
    {
        LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    //--------------------------// 


    protected readonly List<IJtDomainEvent> _domainEvents = [];

    protected void RaiseDomainEvent(IJtDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IReadOnlyList<IJtDomainEvent> GetDomainEvents() => [.. _domainEvents];



}//Cls
