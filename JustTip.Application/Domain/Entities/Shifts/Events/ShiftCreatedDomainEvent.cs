using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Shifts.Events;
internal record ShiftCreatedDomainEvent(Guid EmployeeId) : IJtDomainEvent;
