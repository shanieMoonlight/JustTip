using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Employees.Events;
public record EmployeeCreatedDomainEvent(Guid EmployeeId) : IJtDomainEvent;
