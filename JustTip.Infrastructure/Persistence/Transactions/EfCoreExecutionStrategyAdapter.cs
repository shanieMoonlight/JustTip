using JustTip.Application.Domain.Abstractions.Repos.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace JustTip.Infrastructure.Persistence.Transactions;
internal class EfCoreExecutionStrategyAdapter(IExecutionStrategy strategy) : IJtExecutionStrategy
{
    private readonly IExecutionStrategy _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

    //---------------------------------//

    public Task<TResult> ExecuteAsync<TResult>(Func<CancellationToken, Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);

        // EF Core's IExecutionStrategy.ExecuteAsync passes the DbContext as the first parameter to the delegate.
        // Map that signature to the simpler Func<CancellationToken, Task<TResult>> that the rest of the code expects.
        return _strategy.ExecuteAsync<object, TResult>(
            state: default!,
            operation: (dbContext, state, ct) => operation(ct),
            verifySucceeded: null,
            cancellationToken: cancellationToken);
    }

    //---------------------------------//

    public Task ExecuteAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);

        return _strategy.ExecuteAsync<object, object>(
            state: default!,
            operation: async (dbContext, state, ct) =>
            {
                await operation(ct).ConfigureAwait(false);
                return null!;
            },
            verifySucceeded: null,
            cancellationToken: cancellationToken);
    }

}//Cls