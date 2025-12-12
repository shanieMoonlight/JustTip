using MediatR;

namespace JustTip.Application.Features.Roster.Qry.GetCurrentWeekRoster;


public record GetCurrentWeekRosterQry() : IRequest<GenResult<RosterDto>>;
