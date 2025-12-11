using Jt.Application.Utility.Results;
using JustTip.Application.Domain.Entities;
using JustTip.Application.Mediatr.CQRS;
using MediatR;

namespace JustTip.Application.Mediatr.Behaviours;
public sealed class JtUnitOfWorkPipelineBehaviour<TRequest, TResponse>(IJtUnitOfWork uow)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseJtCommand
    where TResponse : BasicResult
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        TResponse response;


        var strategy = await uow.CreateExecutionStrategyAsync();
        return await strategy.ExecuteAsync(async ct =>
         {
             using var transaction = await uow.BeginTransactionAsync(ct);
             try
             {
                 response = await next(ct);
                 if (response.Succeeded)
                 {
                     await uow.SaveChangesAsync(ct);
                     await transaction.CommitAsync(ct);
                 }
                 else
                 {
                     await transaction.RollbackAsync(ct);
                 }

                 return response;
             }
             catch (Exception)
             {
                 await transaction.RollbackAsync(cancellationToken);
                 throw;
             }
         }, cancellationToken);

    }


}//Cls