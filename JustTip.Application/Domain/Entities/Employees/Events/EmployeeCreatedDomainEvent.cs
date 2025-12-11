using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Employees.Events;
internal record EmployeeCreatedDomainEvent(Guid GridId) : IJtDomainEvent;
