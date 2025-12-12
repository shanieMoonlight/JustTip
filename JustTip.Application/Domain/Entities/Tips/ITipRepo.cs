using JustTip.Application.Domain.Abstractions.Repos;

namespace JustTip.Application.Domain.Entities.Tips;

/// <summary>
/// Defines repository operations for working with <see cref="Tip"/> entities.
/// </summary>
/// <remarks>
/// Implementations of this interface provide CRUD operations (from <see cref="IGenCrudRepo{T}"/>)
/// and additional domain-specific queries for tips such as calculating totals and listing
/// tips within a date range.
/// </remarks>
/// <seealso cref="IGenCrudRepo{Tip}"/>
public interface ITipRepo : IGenCrudRepo<Tip>
{
    /// <summary>
    /// Calculates the total sum of tips between the specified start and end dates (inclusive).
    /// </summary>
    /// <param name="start">The start <see cref="DateTime"/> of the date range to include when summing tips.</param>
    /// <param name="end">The end <see cref="DateTime"/> of the date range to include when summing tips.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that resolves to the total tip amount as a <see cref="decimal"/>.
    /// If no tips exist in the range, the returned value is <c>0</c>.
    /// </returns>
    Task<decimal> GetTotalTipsAsync(DateTime start, DateTime end, CancellationToken cancellationToken);



    /// <summary>
    /// Retrieves a read-only list of <see cref="Tip"/> entities whose associated date falls within the specified range.
    /// </summary>
    /// <param name="start">The inclusive start <see cref="DateTime"/> of the range to query.</param>
    /// <param name="end">The inclusive end <see cref="DateTime"/> of the range to query.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that resolves to an <see cref="IReadOnlyList{Tip}"/> containing matching tips.
    /// If no tips match, the returned list will be empty.
    /// </returns>
    Task<IReadOnlyList<Tip>> ListByDateRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken);
}

