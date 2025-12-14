using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.Features.Roster.Qry.GetRosterByWeek;

public class GetRosterByWeekHandler(IShiftRepo shiftRepo, IRosterUtils rosterUtils)
    : IRequestHandler<GetRosterByWeekQry, GenResult<RosterDto>>
{
    public async Task<GenResult<RosterDto>> Handle(GetRosterByWeekQry request, CancellationToken cancellationToken)
    {
        int weekNumber = request.WeekNumber ?? 0;

        var now = DateTime.UtcNow;
        var thisWeekStart = rosterUtils.GetMostRecentMonday(now);
        var targetWeekStart = thisWeekStart.AddDays(7 * weekNumber);
        var targetWeekEnd = targetWeekStart.AddDays(7);

        var shifts = await shiftRepo.ListByDateRangeWithEmployeeAsync(targetWeekStart, targetWeekEnd, cancellationToken);
        var dto = rosterUtils.ConvertToShiftRosterItemDto(shifts, targetWeekStart, targetWeekEnd);

        return GenResult<RosterDto>.Success(dto);
    }
}
