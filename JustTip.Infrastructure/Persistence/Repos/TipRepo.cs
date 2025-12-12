using JustTip.Application.Domain.Entities.Tips;
using JustTip.Application.Domain.Utils;
using JustTip.Infrastructure.Persistence.Repos.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.Persistence.Repos;
internal class TipRepo(JtDbContext db)
    : AGenCrudRepo<Tip>(db), ITipRepo
{
    public async Task<IReadOnlyList<Tip>> ListByDateRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken)
    {
        if (end <= start)
            return [];

        // ensure UTC
        DateTime startUtc = start.ToUtcDate();
        DateTime endUtc = end.ToUtcDate();

        return await DbCtx.Set<Tip>()
                .AsNoTracking()
                .Where(t => t.DateTime >= startUtc && t.DateTime < endUtc) // [start, end)
                .OrderBy(t => t.DateTime)
                .ToListAsync(cancellationToken);
    }

    //--------------------------//

    public async Task<decimal> GetTotalTipsAsync(DateTime start, DateTime end, CancellationToken cancellationToken)
    {
        // ensure UTC
        DateTime startUtc = start.ToUtcDate();
        DateTime endUtc = end.ToUtcDate();

        var total = await DbCtx.Set<Tip>()
            .AsNoTracking()
            .Where(t => t.DateTime >= startUtc && t.DateTime < endUtc)   // [start, end)
            .Select(t => t.AmountEuros)
            .SumAsync(cancellationToken);

        return total ;
    }



}//Cls
