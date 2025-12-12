using JustTip.Application.Features.Roster;
using JustTip.Application.LocalServices.AppServices;
using MediatR;

namespace JustTip.Application.Features.Tips.Qry.GetCurrentWeekTotalTips;

public class GetCurrentWeekTotalTipsQryHandler(ITipRepo tipRepo, IRosterUtils rosterUtils) 
    : IRequestHandler<GetCurrentWeekTotalTipsQry, GenResult<RosterDto>>
{
    public async Task<GenResult<RosterDto>> Handle(GetCurrentWeekTotalTipsQry request, CancellationToken cancellationToken)
    {
        // Use UTC timezone . DB is in UTC
        DateTime now = DateTime.UtcNow;
        DateTime weekStart = rosterUtils.GetMostRecentMonday(now);
        DateTime weekEnd = weekStart.AddDays(7); // exclusive

        // Repo must return shifts with employee info loaded
        var shifts = await tipRepo.ListByDateRangeAsync(weekStart, weekEnd, cancellationToken);


        var dto = rosterUtils.ConvertToShiftRosterItemDto(shifts, weekStart, weekEnd);


        return GenResult<RosterDto>.Success(dto);
    }

}//Cls