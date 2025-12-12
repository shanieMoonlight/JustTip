using JustTip.Application.Domain.Abstractions.Repos;

namespace JustTip.Application.Domain.Entities.Tips;

/// <summary>
/// Interface for Tip Repository
/// </summary>
public interface ITipRepo : IGenCrudRepo<Tip>
{
    Task<IEnumerable<Shift>> ListByDateRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken);
}

