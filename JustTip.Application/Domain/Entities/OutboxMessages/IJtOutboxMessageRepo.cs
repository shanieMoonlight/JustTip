using JustTip.Application.Domain.Abstractions.Repos;

namespace JustTip.Application.Domain.Entities.OutboxMessages;
public interface IJtOutboxMessageRepo : IGenCrudRepo<JtOutboxMessage>
{

    /// <summary>
    /// Returns up to <paramref name="count"/> outbox messages that have not been processed yet,
    /// ordered by <see cref="JtOutboxMessage.CreatedOnUtc"/> in ascending order (oldest first).
    /// </summary>
    /// <param name="count">Maximum number of messages to return.</param>
    /// <returns>
    /// A read-only list of <see cref="JtOutboxMessage"/> instances that have <see cref="JtOutboxMessage.ProcessedOnUtc"/> == null.
    /// </returns>
    Task<IReadOnlyList<JtOutboxMessage>> ListAllCompletedOlderThanAsync(int count, CancellationToken cancellationToken = default);


    /// <summary>
    /// Returns all outbox messages that were processed earlier than the specified number of days ago.
    /// </summary>
    /// <param name="count">
    /// Number of days to look back. Messages with <see cref="JtOutboxMessage.ProcessedOnUtc"/>
    /// older than UTC now minus <paramref name="count"/> days will be returned.
    /// </param>
    /// <returns>
    /// A read-only list of completed <see cref="JtOutboxMessage"/> instances whose <see cref="JtOutboxMessage.ProcessedOnUtc"/>
    /// is older than the specified threshold.
    /// </returns>
    Task<IReadOnlyList<JtOutboxMessage>> TakeUnprocessedAsync(int count, CancellationToken cancellationToken = default);


}//Int
