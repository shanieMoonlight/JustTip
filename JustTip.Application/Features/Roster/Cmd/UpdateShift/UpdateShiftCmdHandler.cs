namespace JustTip.Application.Features.Roster.Cmd.UpdateShift;
public class UpdateShiftCmdHandler(IShiftRepo _repo) : IJtCommandHandler<UpdateShiftCmd, ShiftDto>
{
    public async Task<GenResult<ShiftDto>> Handle(UpdateShiftCmd request, CancellationToken cancellationToken)
    {

        var dto = request.Dto;
        var id = request.Id ;

        var shift = await _repo.FirstOrDefaultByIdAsync(id);

        if (shift is null)
            return GenResult<ShiftDto>.NotFoundResult(JustTipMsgs.Error.NotFound<Shift>(id));


        shift.Update(dto.Date, dto.StartTimeUtc, dto.EndTimeUtc);

        return GenResult<ShiftDto>.Success(shift.ToDto());

    }


}//Cls
