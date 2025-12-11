using JustTip.Application.Domain.Entities.Common.Events;

namespace JustTip.Application.Domain.Entities.Shifts.Events;
internal record ShiftUpdatedDomainEvent(Guid EmployeeId) : IJtDomainEvent;