namespace JustTip.Application.Features.Tips.Qry.GetTipsTotalByWeek;

public record GetTipsTotalByWeekQry(int? WeekNumber) : IRequest<GenResult<double>>;
