namespace JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;

public class GetUpcomingWeekTotalTipsQryHandler(ITipRepo tipRepo) 
    : IRequestHandler<GetUpcomingWeekTotalTipsQry, GenResult<double>>
{
    public async Task<GenResult<double>> Handle(GetUpcomingWeekTotalTipsQry request, CancellationToken cancellationToken)
    {
        // Use UTC timezone . DB is in UTC
        DateTime weekStartUtc = DateTime.UtcNow.Date;
        DateTime weekEndUtc = weekStartUtc.AddDays(7); // exclusive

        // Repo must return shifts with employee info loaded
        var total = await tipRepo.GetTotalTipsAsync(weekStartUtc, weekEndUtc, cancellationToken);

        return GenResult<double>.Success((double)total);
    }

}//Cls