using Hangfire;
using JustTip.Application.Jobs;
using System.Linq.Expressions;

namespace JustTip.Infrastructure.Jobs;

internal class HfJobService(IRecurringJobManager recurringMgr, IBackgroundJobClient backgroundJobClient) : IJobService
{
    public Task StartRecurringJobAsync<Handler>(string jobId, Expression<Func<Handler, Task>> jobLambda, string cronFrequencyExpression)
        where Handler : AJobHandler
    {
        recurringMgr.AddOrUpdate(jobId, jobLambda, cronFrequencyExpression);
        return Task.CompletedTask;
    }

    //- - - - - - - - - - - - - //

    public Task StopRecurringJobAsync(string jobId)
    {
        recurringMgr.RemoveIfExists(jobId);
        return Task.CompletedTask;
    }

    //- - - - - - - - - - - - - //

    public Task<string> ScheduleJob(Expression<Func<Task>> methodCall, DateTime enqueueAt) =>
        Task.FromResult(backgroundJobClient.Schedule(methodCall, new DateTimeOffset(enqueueAt)));

    //- - - - - - - - - - - - - //

    public Task<bool> CancelScheduledJob(string jobId) => 
        Task.FromResult(backgroundJobClient.Delete(jobId));

}//Cls