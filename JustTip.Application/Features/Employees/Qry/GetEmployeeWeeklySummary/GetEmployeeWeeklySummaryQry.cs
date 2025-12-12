namespace JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummary;

public record GetEmployeeWeeklySummaryQry(Guid EmployeeId) 
    : IRequest<GenResult<EmployeeWeeklySummaryDto>>;
