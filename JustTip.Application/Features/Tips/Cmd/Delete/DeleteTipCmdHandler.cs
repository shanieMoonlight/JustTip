namespace JustTip.Application.Features.Tips.Cmd.Delete;
public class DeleteTipCmdHandler(ITipRepo _repo ) : IJtCommandHandler<DeleteTipCmd>
{

    public async Task<BasicResult> Handle(DeleteTipCmd request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        await _repo.DeleteAsync(id);

        //await uow.SaveChangesAsync(cancellationToken);

        return BasicResult.Success(JustTipMsgs.Info.Deleted<Tip>(id));

    }

}//Cls
