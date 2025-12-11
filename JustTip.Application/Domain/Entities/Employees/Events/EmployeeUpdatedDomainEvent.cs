using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Employees.Events;
public record EmployeeUpdatedDomainEvent(Guid EmployeeId) : IJtDomainEvent;