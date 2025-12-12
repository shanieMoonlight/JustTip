using JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;
using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.Features.Tips.Qry.GetCurrentWeekTotalTips;

public class GetCurrentWeekTotalTipsQryHandler(ITipRepo tipRepo, IRosterUtils rosterUtils) 
    : IRequestHandler<GetCurrentWeekTotalTipsQry, GenResult<double>>
{
    public async Task<GenResult<double>> Handle(GetCurrentWeekTotalTipsQry request, CancellationToken cancellationToken)
    {
        // Use UTC timezone . DB is in UTC
        DateTime now = DateTime.UtcNow;
        DateTime weekStart = rosterUtils.GetMostRecentMonday(now);
        DateTime weekEnd = weekStart.AddDays(7); // exclusive

        // Repo must return shifts with employee info loaded
        var total = await tipRepo.GetTotalTipsAsync(weekStart, weekEnd, cancellationToken);

        return GenResult<double>.Success((double)total);
    }

}//Cls