namespace JustTip.Application.Domain.Entities.Common.Events;


public interface IJtDomainEventEntity 
{
    IReadOnlyList<IJtDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
