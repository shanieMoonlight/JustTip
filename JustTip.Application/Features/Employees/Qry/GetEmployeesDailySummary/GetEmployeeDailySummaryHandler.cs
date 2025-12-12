namespace JustTip.Application.Features.Employees.Qry.GetEmployeesDailySummary;

public class GetEmployeeDailySummaryHandler(
    IEmployeeRepo employeeRepo,
    IShiftRepo shiftRepo,
    ITipRepo tipRepo,
    ITipCalculatorService tipCalculator)
    : IRequestHandler<GetEmployeeDailySummaryQry, GenResult<EmployeeWeeklySummaryDto>>
{
    public async Task<GenResult<EmployeeWeeklySummaryDto>> Handle(GetEmployeeDailySummaryQry request, CancellationToken cancellationToken)
    {
        // Compute day range in UTC from the supplied date. Use ToUtcDate to handle Utc/Local/Unspecified properly.
        var startUtc = request.Date.Date.ToUtcDate();
        var endUtc = startUtc.AddDays(1);

        var employee = await employeeRepo.FirstOrDefaultByIdWithShiftsAsync(request.EmployeeId);
        if (employee == null)
            return GenResult<EmployeeWeeklySummaryDto>.NotFoundResult();

        var empSeconds = await shiftRepo.GetTotalSecondsForEmployeeInRangeAsync(request.EmployeeId, startUtc, endUtc, cancellationToken);
        var totalSeconds = await shiftRepo.GetTotalSecondsAllInRangeAsync(startUtc, endUtc, cancellationToken);
        var totalTips = await tipRepo.GetTotalTipsAsync(startUtc, endUtc, cancellationToken);

        var calc = tipCalculator.Calculate(empSeconds, totalSeconds, totalTips);
        var dto = new EmployeeWeeklySummaryDto(employee.Id, employee.Name, calc.Hours, (double)calc.TipShare);
        return GenResult<EmployeeWeeklySummaryDto>.Success(dto);
    }
}
