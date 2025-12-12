using JustTip.Application.Domain.Abstractions.Repos;

namespace JustTip.Application.Domain.Entities.Shifts;

/// <summary>
/// Interface for Employee Repository
/// </summary>
public interface IShiftRepo : IGenReadRepo<Shift>, IGenUpdateRepo<Shift>
{
    Task<long> GetTotalSecondsAllInRangeAsync(DateTime startUtc, DateTime endUtc, CancellationToken cancellationToken);
    Task<long> GetTotalSecondsForEmployeeInRangeAsync(Guid employeeId, DateTime startUtc, DateTime endUtc, CancellationToken cancellationToken);
    Task<IReadOnlyList<Shift>> ListByDateRangeWithEmployeeAsync(DateTime start, DateTime end, CancellationToken cancellationToken);
}

