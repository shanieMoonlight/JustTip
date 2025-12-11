using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Employees.Events;
internal record ShiftRemovedDomainEvent(Guid GridId, Guid RegionId) : IJtDomainEvent;