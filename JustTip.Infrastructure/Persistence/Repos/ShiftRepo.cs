using JustTip.Application.Domain.Entities.Shifts;
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

}
