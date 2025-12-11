using System.Linq.Expressions;

namespace JustTip.Application.Jobs;


/// <summary>
/// Manage (Start/Stop) automated jobs
/// </summary>
public interface IJobService
{
    /// <summary>
    /// Starts or updates a recurring job with the specified job ID, execution lambda, and CRON frequency expression.
    /// </summary>
    /// <param name="jobId">The unique identifier for the recurring job.</param>
    /// <param name="jobLambda">The job logic to execute.</param>
    /// <param name="cronFrequencyExpression">The CRON expression specifying the job's schedule.</param>
    public Task StartRecurringJobAsync<Handler>(
        string jobId, 
        Expression<Func<Handler, Task>> jobLambda, 
        string cronFrequencyExpression)
        where Handler : AJobHandler;


    /// <summary>
    /// Stops and removes a recurring job with the specified job ID.
    /// </summary>
    /// <param name="jobId">The unique identifier for the recurring job.</param>
    Task StopRecurringJobAsync(string jobId);


    //- - - - - - - - - - - - - //


    /// <summary>
    /// Cancels a scheduled (one-off) job with the specified job ID.
    /// </summary>
    /// <param name="jobId">The unique identifier for the scheduled job.</param>
    /// <returns>True if the job was successfully cancelled; otherwise, false.</returns>
    Task<bool> CancelScheduledJob(string jobId);


    /// <summary>
    /// Schedules a job to be executed at a future date and time.
    /// </summary>
    /// <param name="methodCall">The job logic to execute.</param>
    /// <param name="enqueueAt">The date and time at which to enqueue the job.</param>
    /// <returns>The job ID of the scheduled job.</returns>
    Task<string> ScheduleJob(Expression<Func<Task>> methodCall, DateTime enqueueAt);

}//Cls
