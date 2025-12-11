using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Application.Logging;
using Microsoft.Extensions.Logging;

namespace JustTip.Application.Jobs.OutboxMsgs;

public class ProcessOldOutboxMsgsJob(IJtUnitOfWork uow, ILogger<ProcessOldOutboxMsgsJob> logger)
    : AJobHandler("JT_OLD_OUTBOX_MSGS")
{


    public override async Task HandleAsync()
    {
        try
        {
            IJtOutboxMessageRepo _repo = uow.OutboxMessageRepo;

            var completedResults = await _repo.ListAllCompletedOlderThanAsync(21);
            await _repo.DeleteRangeAsync(completedResults);

            await uow.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // If persisting the error fails, log both exceptions.
            logger.LogError(JtLoggingEvents.JOBS.OLD_OUTBOX_MSGS_PROCESSING, ex,
                "Failed to process Outbox Message Job {JobId}. Original error: {OriginalError}",
                 JobId, ex.ToString());
        }

    }


}//Cls
