using JustTip.Application.Domain.Abstractions.Repos.Transactions;
using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Application.Domain.Entities.Tips;
using JustTip.Infrastructure.Persistence.Transactions;

namespace JustTip.Infrastructure.Persistence.Repos;

internal class JtUnitOfWork(
    JtDbContext db,
    IEmployeeRepo employeeRepo,
    ITipRepo tipRepo,
    IJtOutboxMessageRepo outboxMessageRepo)
    : IJtUnitOfWork
{

    //----------------------//

    public IEmployeeRepo EmployeeRepo => employeeRepo;
    public IJtOutboxMessageRepo OutboxMessageRepo => outboxMessageRepo;
    public ITipRepo TipRepo => tipRepo;

    //----------------------//

    public void Dispose()
    {
        db.Dispose();
        GC.SuppressFinalize(this);

    }

    //----------------------//

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await db.SaveChangesAsync(cancellationToken);

    //----------------------//

    public async Task<IJtTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var dbContextTransaction = await db.Database.BeginTransactionAsync(cancellationToken);
        return new EfCoreTransaction(dbContextTransaction);
    }

    //----------------------//

    public Task<IJtExecutionStrategy> CreateExecutionStrategyAsync()
    {
        var dbContextExecutionStrategy = db.Database.CreateExecutionStrategy();
        var adapter = new EfCoreExecutionStrategyAdapter(dbContextExecutionStrategy);
        return Task.FromResult<IJtExecutionStrategy>(adapter);
    }

}//Cls
