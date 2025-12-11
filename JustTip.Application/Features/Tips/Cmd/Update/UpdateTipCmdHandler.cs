namespace JustTip.Application.Features.Tips.Cmd.Update;
public class UpdateTipCmdHandler(ITipRepo _repo ) : IJtCommandHandler<UpdateTipCmd, TipDto>
{
    public async Task<GenResult<TipDto>> Handle(UpdateTipCmd request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        if (dto == null)
            return GenResult<TipDto>.BadRequestResult(JustTipMsgs.Error.NO_DATA_SUPPLIED);

        var mdl = await _repo.FirstOrDefaultByIdAsync(dto.Id);
        if (mdl == null)
            return GenResult<TipDto>.NotFoundResult(JustTipMsgs.Error.NotFound<Tip>(dto.Id));

        mdl.Update(dto);

        var entity = await _repo.UpdateAsync(mdl);

        //await uow.SaveChangesAsync(cancellationToken);

        return GenResult<TipDto>.Success(entity!.ToDto());

    }

}//Cls
