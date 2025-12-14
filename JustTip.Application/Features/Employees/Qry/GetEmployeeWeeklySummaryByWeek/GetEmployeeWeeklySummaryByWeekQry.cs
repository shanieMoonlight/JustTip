using Jt.Application.Utility.Results;

namespace JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummaryByWeek;

public record GetEmployeeWeeklySummaryByWeekQry(Guid EmployeeId, int? WeekNumber) : IRequest<GenResult<EmployeeWeeklySummaryDto>>;
