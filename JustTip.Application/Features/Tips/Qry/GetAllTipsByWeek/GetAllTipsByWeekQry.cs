namespace JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;


public record GetAllTipsByWeekQry(int? WeekNumber = 0) : IRequest<GenResult<List<TipDto>>>;
