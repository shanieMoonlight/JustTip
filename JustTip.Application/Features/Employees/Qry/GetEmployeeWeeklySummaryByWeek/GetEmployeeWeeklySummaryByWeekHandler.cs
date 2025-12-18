using JustTip.Application.LocalServices.AppServices;
using System.Diagnostics;

namespace JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummaryByWeek;

public class GetEmployeeWeeklySummaryByWeekHandler(
    IEmployeeRepo employeeRepo,
    IShiftRepo shiftRepo,
    ITipRepo tipRepo,
    IRosterUtils rosterUtils,
    ITipCalculatorService tipCalculator)
    : IRequestHandler<GetEmployeeWeeklySummaryByWeekQry, GenResult<EmployeeWeeklySummaryDto>>
{
    public async Task<GenResult<EmployeeWeeklySummaryDto>> Handle(GetEmployeeWeeklySummaryByWeekQry request, CancellationToken cancellationToken)
    {
        //We are always looking for previous or current weeks
        int weekNumber = Math.Abs(request.WeekNumber ?? 0);


        var now = DateTime.UtcNow;
        var mostRecentMonday = rosterUtils.GetMostRecentMonday(now);
        var targetWeekStart = mostRecentMonday.AddDays(-7 * weekNumber);
        var targetWeekEnd = targetWeekStart.AddDays(7);

        var employee = await employeeRepo.FirstOrDefaultByIdWithShiftsAsync(request.EmployeeId);
        if (employee == null)
            return GenResult<EmployeeWeeklySummaryDto>.NotFoundResult();

        var empSeconds = await shiftRepo.GetTotalSecondsForEmployeeInRangeAsync(request.EmployeeId, targetWeekStart, targetWeekEnd, cancellationToken);
        var totalSeconds = await shiftRepo.GetTotalSecondsAllInRangeAsync(targetWeekStart, targetWeekEnd, cancellationToken);
        var totalTips = await tipRepo.GetTotalTipsAsync(targetWeekStart, targetWeekEnd, cancellationToken);


        var calc = tipCalculator.Calculate(empSeconds, totalSeconds, totalTips);
        var dto = new EmployeeWeeklySummaryDto(
            employee.Id, 
            employee.Name, 
            calc.Hours, 
            (double)calc.TipShare,
            rosterUtils.ToLocalTime(targetWeekStart), 
            rosterUtils.ToLocalTime(targetWeekEnd));
        return GenResult<EmployeeWeeklySummaryDto>.Success(dto);
    }
}
