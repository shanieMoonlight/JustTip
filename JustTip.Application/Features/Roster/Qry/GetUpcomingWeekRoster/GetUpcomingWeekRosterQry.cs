using MediatR;

namespace JustTip.Application.Features.Roster.Qry.GetUpcomingWeekRoster;




public record GetUpcomingWeekRosterQry() : IRequest<GenResult<RosterDto>>;
