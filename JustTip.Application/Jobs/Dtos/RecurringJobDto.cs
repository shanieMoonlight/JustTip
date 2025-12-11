namespace JustTip.Application.Jobs.Dtos;


public class JtRecurringJobDto(
    string id,
    string cron,
    string queue,
    JtJobDto job,
    Exception loadException,
    DateTime? nextExecution,
    string lastJobId,
    string lastJobState,
    DateTime? lastExecution,
    DateTime? createdAt,
    bool removed,
    string timeZoneId,
    string error,
    int retryAttempt)
{
    public string Id { get; set; } = id;
    public string Cron { get; set; } = cron;
    public string Queue { get; set; } = queue;
    public JtJobDto Job { get; set; } = job;
    public Exception LoadException { get; set; } = loadException;
    public DateTime? NextExecution { get; set; } = nextExecution;
    public string LastJobId { get; set; } = lastJobId;
    public string LastJobState { get; set; } = lastJobState;
    public DateTime? LastExecution { get; set; } = lastExecution;
    public DateTime? CreatedAt { get; set; } = createdAt;
    public bool Removed { get; set; } = removed;
    public string TimeZoneId { get; set; } = timeZoneId;
    public string Error { get; set; } = error;
    public int RetryAttempt { get; set; } = retryAttempt;
}
