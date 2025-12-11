using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.Common.Events;
using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Application.Logging;
using JustTip.Application.Utility;
using JustTip.Application.Utility.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace JustTip.Application.Jobs.OutboxMsgs;


public class ProcessOutboxMsgJob(IJtUnitOfWork uow, IPublisher publisher, ILogger<ProcessOutboxMsgJob> logger)
    : AJobHandler("GB_OUTBOX_MSG_JOB")
{

    public override async Task HandleAsync()
    {
        IJtOutboxMessageRepo _repo = uow.OutboxMessageRepo;
        try
        {
            var msgs = await _repo.TakeUnprocessedAsync(30);

            if (!msgs.Any())
                return;

            foreach (var msg in msgs)
                await ProcessAsync(msg);

        }
        catch (Exception ex)
        {            
            // If persisting the error fails, log both exceptions.
            logger.LogError(JtLoggingEvents.JOBS.OUTBOX_PROCESSING, ex,
                "Failed to process Outbox Message Job {JobId}. Original error: {OriginalError}",
                 JobId, ex.ToString());

        }

    }


    //----------------------------------//


    private async Task ProcessAsync(JtOutboxMessage msg)
    {
        var repo = uow.OutboxMessageRepo;
        try
        {
            var domainEv = JsonConvert.DeserializeObject<IJtDomainEvent>(msg.ContentJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new SisoJsonDefaultContractResolver()
            });

            if (domainEv == null)
            {
                logger.LogError(JtLoggingEvents.JOBS.OUTBOX_PROCESSING, "{msg}", JtMsgs.Error.Jobs.MISSING_OUTBOX_CONTENT(msg));
                return;
            }

            await publisher.Publish(domainEv);

            msg.SetProcessed();
            await repo.UpdateAsync(msg);
            await uow.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // If persisting the error fails, log both exceptions.
            logger.LogError(JtLoggingEvents.JOBS.OUTBOX_PROCESSING, ex,
                "Failed to persist error for outbox message {OutboxId}. Original error: {OriginalError}",
                msg.Id, ex.ToString());
        }
    }

}//Cls
