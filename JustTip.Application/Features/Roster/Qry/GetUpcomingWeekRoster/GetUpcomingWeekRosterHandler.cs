using JustTip.Application.LocalServices.AppServices;
using MediatR;

namespace JustTip.Application.Features.Roster.Qry.GetUpcomingWeekRoster;

public class GetUpcomingWeekRosterHandler(IShiftRepo shiftRepo, IRosterUtils rosterUtils) 
    : IRequestHandler<GetUpcomingWeekRosterQry, GenResult<RosterDto>>
{
    public async Task<GenResult<RosterDto>> Handle(GetUpcomingWeekRosterQry request, CancellationToken cancellationToken)
    {
        // Use UTC day-aligned range for the query
        DateTime weekStartUtc = DateTime.UtcNow.Date;
        DateTime weekEndUtc = weekStartUtc.AddDays(7); // exclusive

        // Repo must return shifts with employee info loaded
        var shifts = await shiftRepo.ListByDateRangeWithEmployeeAsync(weekStartUtc, weekEndUtc, cancellationToken);

        var dto = rosterUtils.ConvertToShiftRosterItemDto(shifts, weekStartUtc, weekEndUtc);


        return GenResult<RosterDto>.Success(dto);
    }

}//Cls