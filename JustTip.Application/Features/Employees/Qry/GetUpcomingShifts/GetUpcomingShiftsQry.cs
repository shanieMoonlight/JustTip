using Jt.Application.Utility.Results;
using JustTip.Application.Features.Roster;

namespace JustTip.Application.Features.Employees.Qry.GetUpcomingShifts;

public record GetUpcomingShiftsQry(Guid EmployeeId) : IRequest<GenResult<List<ShiftRosterItemDto>>>;
