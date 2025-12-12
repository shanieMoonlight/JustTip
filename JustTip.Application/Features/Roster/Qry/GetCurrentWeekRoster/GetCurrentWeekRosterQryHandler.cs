using JustTip.Application.LocalServices.AppServices;
using MediatR;

namespace JustTip.Application.Features.Roster.Qry.GetCurrentWeekRoster;

public class GetCurrentWeekRosterQryHandler(IShiftRepo shiftRepo, IRosterUtils rosterUtils) 
    : IRequestHandler<GetCurrentWeekRosterQry, GenResult<RosterDto>>
{
    public async Task<GenResult<RosterDto>> Handle(GetCurrentWeekRosterQry request, CancellationToken cancellationToken)
    {
        // Use UTC timezone . DB is in UTC
        DateTime now = DateTime.UtcNow;
        DateTime weekStart = rosterUtils.GetMostRecentMonday(now);
        DateTime weekEnd = weekStart.AddDays(7); // exclusive

        // Repo must return shifts with employee info loaded
        var shifts = await shiftRepo.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, cancellationToken);


        var dto = rosterUtils.ConvertToShiftRosterItemDto(shifts, weekStart, weekEnd);


        return GenResult<RosterDto>.Success(dto);
    }

}//Cls