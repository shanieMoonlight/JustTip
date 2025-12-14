namespace JustTip.Application.Features.Employees;

public record EmployeeWeeklySummaryDto(
    Guid EmployeeId,
    string EmployeeName,
    double HoursWorked,
    double TipShare,
    DateTime RangeStartDate,
    DateTime RangeEndDate
);
