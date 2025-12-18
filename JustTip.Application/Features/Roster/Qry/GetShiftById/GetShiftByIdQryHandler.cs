namespace JustTip.Application.Features.Roster.Qry.GetShiftById;
internal class GetShiftByIdQryHandler(IShiftRepo repo) : IJtQueryHandler<GetShiftByIdQry, ShiftWithEmployeeDto>
{

    public async Task<GenResult<ShiftWithEmployeeDto>> Handle(GetShiftByIdQry request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var mdl = await repo.FirstOrDefaultByIdWithEmployeeAsync(id);
        if (mdl is null)
            return GenResult<ShiftWithEmployeeDto>.NotFoundResult(JustTipMsgs.Error.NotFound<Shift>(id));

        return GenResult<ShiftWithEmployeeDto>.Success(new ShiftWithEmployeeDto(mdl));

    }


}//Cls
