namespace JustTip.Application.Features.Tips.Qry.GetById;
internal class GetTipByIdQryHandler(ITipRepo repo) : IJtQueryHandler<GetTipByIdQry, TipDto>
{

    public async Task<GenResult<TipDto>> Handle(GetTipByIdQry request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var mdl = await repo.FirstOrDefaultByIdAsync(id);
        if (mdl is null)
            return GenResult<TipDto>.NotFoundResult(JustTipMsgs.Error.NotFound<Tip>(id));

        return GenResult<TipDto>.Success(mdl.ToDto());

    }


}//Cls
