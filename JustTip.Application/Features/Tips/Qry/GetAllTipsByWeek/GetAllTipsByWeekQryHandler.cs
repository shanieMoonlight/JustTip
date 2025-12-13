namespace JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;

using Jt.Application.Utility.Results;

public class GetAllTipsByWeekQryHandler(ITipRepo tipRepo) 
    : IRequestHandler<GetAllTipsByWeekQry, GenResult<List<TipDto>>>
{
    public async Task<GenResult<List<TipDto>>> Handle(GetAllTipsByWeekQry request, CancellationToken cancellationToken)
    {
        //Just in case, ensure weekNumber is non-negative
        int weekNumber = Math.Abs(request.WeekNumber ?? 0);

        // Compute the target week's Monday (UTC) based on weekNumber
        var now = DateTime.UtcNow;
        var thisWeekStart = GetMostRecentMonday(now);
        var targetWeekStart = thisWeekStart.AddDays(-7 * weekNumber);
        var targetWeekEnd = targetWeekStart.AddDays(7);

        var tips = await tipRepo.ListByDateRangeAsync(targetWeekStart, targetWeekEnd, cancellationToken);

        var dtos = tips.Select(t => new TipDto(t)).ToList();
        return GenResult<List<TipDto>>.Success(dtos);
    }

    private static DateTime GetMostRecentMonday(DateTime now)
    {
        var dt = now.Date;
        int diff = ((int)dt.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        return dt.AddDays(-diff);
    }

}//Cls
