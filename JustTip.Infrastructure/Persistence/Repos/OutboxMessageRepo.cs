using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Infrastructure.Persistence;
using JustTip.Infrastructure.Persistence.Repos.Abstractions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Repository for <see cref="JtOutboxMessage"/> entities.
/// </summary>
/// <remarks>
/// Inherits general CRUD behavior from <see cref="AGenCrudRepo{T}"/> and implements
/// <see cref="IJtOutboxMessageRepo"/> to provide outbox-specific queries such as
/// retrieving unprocessed messages and listing completed messages older than a given age.
/// </remarks>
internal class JtOutboxMessageRepo(JtDbContext db)
    : AGenCrudRepo<JtOutboxMessage>(db), IJtOutboxMessageRepo
{

    /// <summary>
    /// Returns up to <paramref name="count"/> outbox messages that have not been processed yet,
    /// ordered by <see cref="JtOutboxMessage.CreatedOnUtc"/> in ascending order (oldest first).
    /// </summary>
    /// <param name="count">Maximum number of messages to return.</param>
    /// <returns>
    /// A read-only list of <see cref="JtOutboxMessage"/> instances that have <see cref="JtOutboxMessage.ProcessedOnUtc"/> == null.
    /// </returns>
    public async Task<IReadOnlyList<JtOutboxMessage>> TakeUnprocessedAsync(int count, CancellationToken cancellationToken = default)
    {
        var safeCount = Math.Max(0, count); 
        return await DbCtx.OutboxMessages
                .Where(m => m.ProcessedOnUtc == null)
                .OrderBy(m => m.CreatedOnUtc)
                .Take(safeCount)
                .ToListAsync(cancellationToken: cancellationToken);
    }

    //--------------------------// 

    /// <summary>
    /// Returns all outbox messages that were processed earlier than the specified number of days ago.
    /// </summary>
    /// <param name="days">
    /// Number of days to look back. Messages with <see cref="JtOutboxMessage.ProcessedOnUtc"/>
    /// older than UTC now minus <paramref name="count"/> days will be returned. 
    /// </param>
    /// <returns>
    /// A read-only list of completed <see cref="JtOutboxMessage"/> instances whose <see cref="JtOutboxMessage.ProcessedOnUtc"/>
    /// is older than the specified threshold.
    /// </returns>
    public async Task<IReadOnlyList<JtOutboxMessage>> ListAllCompletedOlderThanAsync(
        int days, CancellationToken cancellationToken = default)
    {
        var safeCount = Math.Max(0, days);
        return await DbCtx.OutboxMessages
                .Where(m => m.ProcessedOnUtc != null)
                .Where(m => m.ProcessedOnUtc < DateTime.UtcNow.AddDays(-safeCount))
                .OrderBy(m => m.ProcessedOnUtc)
                .ToListAsync(cancellationToken: cancellationToken);
    }

}//Cls
