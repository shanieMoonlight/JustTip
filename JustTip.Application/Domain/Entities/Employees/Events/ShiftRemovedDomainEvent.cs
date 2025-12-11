using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Employees.Events;
public record ShiftRemovedDomainEvent(Guid EmployeeId, Guid ShiftId) : IJtDomainEvent;