using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.Features.Employees.Qry.GetEmployeePreviousWeeklySummary;

public class GetEmployeePreviousWeeklySummaryHandler(
    IEmployeeRepo employeeRepo,
    IShiftRepo shiftRepo,
    ITipRepo tipRepo,
    IRosterUtils rosterUtils,
    ITipCalculatorService tipCalculator)
    : IRequestHandler<GetEmployeePreviousWeeklySummaryQry, GenResult<EmployeeWeeklySummaryDto>>
{
    public async Task<GenResult<EmployeeWeeklySummaryDto>> Handle(GetEmployeePreviousWeeklySummaryQry request, CancellationToken cancellationToken)
    {
        if (request.WeeksAgo < 0)
            return GenResult<EmployeeWeeklySummaryDto>.BadRequestResult("WeeksAgo must be >= 0");

        // Compute week start by going back WeeksAgo weeks from most recent Monday
        var now = DateTime.UtcNow;
        var thisWeekStart = rosterUtils.GetMostRecentMonday(now);
        var targetWeekStart = thisWeekStart.AddDays(-7 * request.WeeksAgo);
        var targetWeekEnd = targetWeekStart.AddDays(7);

        var employee = await employeeRepo.FirstOrDefaultByIdWithShiftsAsync(request.EmployeeId);
        if (employee == null)
            return GenResult<EmployeeWeeklySummaryDto>.NotFoundResult();

        var empSeconds = await shiftRepo.GetTotalSecondsForEmployeeInRangeAsync(request.EmployeeId, targetWeekStart, targetWeekEnd, cancellationToken);
        var totalSeconds = await shiftRepo.GetTotalSecondsAllInRangeAsync(targetWeekStart, targetWeekEnd, cancellationToken);
        var totalTips = await tipRepo.GetTotalTipsAsync(targetWeekStart, targetWeekEnd, cancellationToken);

        var calc = tipCalculator.Calculate(empSeconds, totalSeconds, totalTips);
        var dto = new EmployeeWeeklySummaryDto(employee.Id, employee.Name, calc.Hours, (double)calc.TipShare);
        return GenResult<EmployeeWeeklySummaryDto>.Success(dto);
    }
}
