namespace JustTip.Application.Features.JtOutboxMessages.Qry.GetById;
internal class GetJtOutboxMessageByIdQryHandler(IJtOutboxMessageRepo repo) : IJtQueryHandler<GetJtOutboxMessageByIdQry, JtOutboxMessageDto>
{

    public async Task<GenResult<JtOutboxMessageDto>> Handle(GetJtOutboxMessageByIdQry request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var mdl = await repo.FirstOrDefaultByIdAsync(id);
        if (mdl is null)
            return GenResult<JtOutboxMessageDto>.NotFoundResult(JtMsgs.Error.NotFound<JtOutboxMessage>(id));

        return GenResult<JtOutboxMessageDto>.Success(mdl.ToDto());

    }//Handle


}//Cls
