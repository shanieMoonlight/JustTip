namespace JustTip.Application.Features.JtOutboxMessages.Qry.GetAll;
internal class GetAllJtOutboxMessagesQryHandler(IJtOutboxMessageRepo repo) : IJtQueryHandler<GetAllJtOutboxMessagesQry, IEnumerable<JtOutboxMessageDto>>
{

    public async Task<GenResult<IEnumerable<JtOutboxMessageDto>>> Handle(GetAllJtOutboxMessagesQry request, CancellationToken cancellationToken)
    {
        var mdls = await repo.ListAllAsync();
        var dtos = mdls.Select(mdl => mdl.ToDto());
        return GenResult<IEnumerable<JtOutboxMessageDto>>.Success(dtos);

    }

}//Cls
