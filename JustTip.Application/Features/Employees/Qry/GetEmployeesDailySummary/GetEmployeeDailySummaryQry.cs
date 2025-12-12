namespace JustTip.Application.Features.Employees.Qry.GetEmployeesDailySummary;

public record GetEmployeeDailySummaryQry(Guid EmployeeId, DateTime Date) : IRequest<GenResult<EmployeeWeeklySummaryDto>>;
