using JustTip.Application.LocalServices.AppServices;
using JustTip.Application.LocalServices.Abs;

namespace JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummary;

public class GetEmployeeWeeklySummaryQryHandler( 
    IEmployeeRepo employeeRepo, 
    IShiftRepo shiftRepo, 
    ITipRepo tipRepo, 
    IRosterUtils rosterUtils,
    ITipCalculatorService tipCalculator)
    : IRequestHandler<GetEmployeeWeeklySummaryQry, GenResult<EmployeeWeeklySummaryDto>>
{
    public async Task<GenResult<EmployeeWeeklySummaryDto>> Handle(GetEmployeeWeeklySummaryQry request, CancellationToken cancellationToken)
    {
        var weekStartUtc = rosterUtils.GetMostRecentMonday(DateTime.UtcNow);
        var weekEndUtc = DateTime.UtcNow; // or weekStartUtc.AddDays(7) if you want full-week
       
        // Get employee to include name (or return not found)
        var employee = await employeeRepo.FirstOrDefaultByIdWithShiftsAsync(request.EmployeeId);
        if (employee == null)
            return GenResult<EmployeeWeeklySummaryDto>.NotFoundResult();

        // Sum seconds for this employee in range
        var empSeconds = await shiftRepo.GetTotalSecondsForEmployeeInRangeAsync(request.EmployeeId, weekStartUtc, weekEndUtc, cancellationToken);

        // Sum all seconds for everyone in range (for tip split)
        var totalSeconds = await shiftRepo.GetTotalSecondsAllInRangeAsync(weekStartUtc, weekEndUtc, cancellationToken);

        // Total tips in the range
        var totalTips = await tipRepo.GetTotalTipsAsync(weekStartUtc, weekEndUtc, cancellationToken);

        var calc = tipCalculator.Calculate(empSeconds, totalSeconds, totalTips);
        var dto = new EmployeeWeeklySummaryDto(employee.Id, employee.Name, calc.Hours, (double)calc.TipShare);
        return GenResult<EmployeeWeeklySummaryDto>.Success(dto);
    }
}
