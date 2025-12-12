namespace JustTip.Application.Features.Employees.Qry.GetEmployeePreviousWeeklySummary;

public record GetEmployeePreviousWeeklySummaryQry(Guid EmployeeId, int WeeksAgo) : IRequest<GenResult<EmployeeWeeklySummaryDto>>;
