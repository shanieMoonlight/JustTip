using Jt.Application.Utility.Results;

namespace JustTip.Application.Features.Roster.Qry.GetRosterByWeek;

public record GetRosterByWeekQry(int? WeekNumber) : IRequest<GenResult<RosterDto>>;
