namespace JustTip.Application.Jobs;
public abstract class AJobHandler(string jobId)
{
    public string JobId { get; set; } = jobId;
    public abstract Task HandleAsync();
}