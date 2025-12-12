using JustTip.Application.Features.Roster;
using MediatR;

namespace JustTip.Application.Features.Tips.Qry.GetCurrentWeekTotalTips;


public record GetCurrentWeekTotalTipsQry() : IRequest<GenResult<RosterDto>>;
