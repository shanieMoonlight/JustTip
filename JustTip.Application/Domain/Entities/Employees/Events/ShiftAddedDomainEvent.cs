using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Employees.Events;
public record ShiftAddedDomainEvent(Guid EmployeeId, Guid ShiftId) : IJtDomainEvent;