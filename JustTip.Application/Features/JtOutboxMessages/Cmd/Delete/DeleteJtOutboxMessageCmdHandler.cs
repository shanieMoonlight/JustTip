using JustTip.Application.Domain.Entities.OutboxMessages;

namespace JustTip.Application.Features.JtOutboxMessages.Cmd.Delete;
public class DeleteJtOutboxMessageCmdHandler(IJtOutboxMessageRepo _repo ) : IJtCommandHandler<DeleteJtOutboxMessageCmd>
{

    public async Task<BasicResult> Handle(DeleteJtOutboxMessageCmd request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        await _repo.DeleteAsync(id);

        //await uow.SaveChangesAsync(cancellationToken);

        return BasicResult.Success(JtMsgs.Info.Deleted<JtOutboxMessage>(id));

    }

}//Cls
