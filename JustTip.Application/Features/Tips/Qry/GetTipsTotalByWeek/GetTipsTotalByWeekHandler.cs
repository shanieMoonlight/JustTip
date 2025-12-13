using Jt.Application.Utility.Results;
using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.Features.Tips.Qry.GetTipsTotalByWeek;

public class GetTipsTotalByWeekHandler(ITipRepo tipRepo, IRosterUtils rosterUtils)
    : IRequestHandler<GetTipsTotalByWeekQry, GenResult<double>>
{
    public async Task<GenResult<double>> Handle(GetTipsTotalByWeekQry request, CancellationToken cancellationToken)
    {
        int weekNumber = Math.Abs(request.WeekNumber ?? 0);

        var now = DateTime.UtcNow;
        var thisWeekStart = rosterUtils.GetMostRecentMonday(now);
        var targetWeekStart = thisWeekStart.AddDays(-7 * weekNumber);
        var targetWeekEnd = targetWeekStart.AddDays(7);

        var total = await tipRepo.GetTotalTipsAsync(targetWeekStart, targetWeekEnd, cancellationToken);

        return GenResult<double>.Success((double)total);
    }

}
