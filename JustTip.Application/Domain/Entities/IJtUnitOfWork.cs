using JustTip.Application.Domain.Abstractions.Repos.Transactions;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Application.Domain.Entities.OutboxMessages;

namespace JustTip.Application.Domain.Entities;
public interface IJtUnitOfWork : IDisposable
{
    IEmployeeRepo EmployeeRepo { get; }
    IJtOutboxMessageRepo OutboxMessageRepo { get; }

    //--------------------------// 

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IJtTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<IJtExecutionStrategy> CreateExecutionStrategyAsync();

}//int
