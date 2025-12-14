using JustTip.Application.Features.Roster;
using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.Features.Employees.Qry.GetUpcomingShifts;

public class GetUpcomingShiftsQryHandler(
    IShiftRepo shiftRepo,
    IRosterUtils rosterUtils)
    : IRequestHandler<GetUpcomingShiftsQry, GenResult<List<ShiftRosterItemDto>>>
{
    public async Task<GenResult<List<ShiftRosterItemDto>>> Handle(GetUpcomingShiftsQry request, CancellationToken cancellationToken)
    {
        // We'll return shifts for the next 7 days starting at UTC midnight
        var startUtc = DateTime.UtcNow.Date;

        // Query shifts in the range (repo returns shifts with employee loaded)
        var shifts = await shiftRepo.ListAllFromByDateWithEmployeeAsync(startUtc, cancellationToken);

        var items = shifts
            .Where(s => s.EmployeeId == request.EmployeeId)
            .OrderBy(s => s.Date)
            .ThenBy(s => s.StartTimeUtc)
            .Select(s => new ShiftRosterItemDto(
                s.Id,
                s.EmployeeId,
                s.Employee!.Name,
                s.Date.Date,
                rosterUtils.ToLocalTime(s.StartTimeUtc),
                rosterUtils.ToLocalTime(s.EndTimeUtc)
            ))
            .ToList();

        return GenResult<List<ShiftRosterItemDto>>.Success(items);
    }
}
