using JustTip.Application.Domain.Entities.Shifts;
using JustTip.Application.Domain.Utils;
using JustTip.Infrastructure.Persistence.Repos.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.Persistence.Repos;
internal class ShiftRepo(JtDbContext db) : AGenCrudRepo<Shift>(db), IShiftRepo
{

    public async Task<IReadOnlyList<Shift>> ListByDateRangeWithEmployeeAsync(DateTime start, DateTime end, CancellationToken cancellationToken)
    {
        if (end < start)
            return [];

        return await DbCtx.Shifts
            .AsNoTracking()
            .Where(e => e.StartTimeUtc >= start && e.EndTimeUtc <= end)
            .Include(e => e.Employee)
            .ToListAsync(cancellationToken);
    }

    //----------------------//

    public async Task<long> GetTotalSecondsForEmployeeInRangeAsync(Guid employeeId, DateTime startUtc, DateTime endUtc, CancellationToken cancellationToken)
    {
        startUtc = startUtc.ToUtcDate();
        endUtc = endUtc.ToUtcDate();
        // Fetch overlapping shifts (only start/end columns) and perform clipping + summing in-memory
        var shifts = await DbCtx.Set<Shift>()
            .AsNoTracking()
            .Where(s => s.EmployeeId == employeeId)
            .Where(s => s.EndTimeUtc > startUtc)  // shift ends after range start (overlaps)
            .Where(s => s.StartTimeUtc < endUtc)  // shift starts before range end (overlaps)
            .Select(s => new { s.StartTimeUtc, s.EndTimeUtc })
            .ToListAsync(cancellationToken);

        //Could do this on DB side with raw SQL or Ef.Functions.DateDiffSecond, etc., depending on the DB, but this is simpler and shifts are not that many usually.
        //And we haven't decided to use any specific DB that would have specific SQL syntax.
        var pairs = shifts.Select(s => (s.StartTimeUtc, s.EndTimeUtc));
        return pairs.SumClippedSeconds(startUtc, endUtc);
    }

    //----------------------//

    public async Task<long> GetTotalSecondsAllInRangeAsync(DateTime startUtc, DateTime endUtc, CancellationToken cancellationToken)
    {
        startUtc = startUtc.ToUtcDate();
        endUtc = endUtc.ToUtcDate();

        var shifts = await DbCtx.Set<Shift>()
            .AsNoTracking()
            .Where(s => s.EndTimeUtc > startUtc && s.StartTimeUtc < endUtc)
            .Select(s => new { s.StartTimeUtc, s.EndTimeUtc })
            .ToListAsync(cancellationToken);

        //Could do this on DB side with raw SQL or Ef.Functions.DateDiffSecond, etc., depending on the DB, but this is simpler and shifts are not that many usually.
        //And we haven't decided to use any specific DB that would have specific SQL syntax.
        var pairs = shifts.Select(s => (s.StartTimeUtc, s.EndTimeUtc));
        return pairs.SumClippedSeconds(startUtc, endUtc);
    }

}
