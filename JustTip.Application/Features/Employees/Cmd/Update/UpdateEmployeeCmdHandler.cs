namespace JustTip.Application.Features.Employees.Cmd.Update;
public class UpdateEmployeeCmdHandler(IEmployeeRepo _repo ) : IJtCommandHandler<UpdateEmployeeCmd, EmployeeDto>
{
    public async Task<GenResult<EmployeeDto>> Handle(UpdateEmployeeCmd request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        if (dto == null)
            return GenResult<EmployeeDto>.BadRequestResult(JustTipMsgs.Error.NO_DATA_SUPPLIED);

        var mdl = await _repo.FirstOrDefaultByIdAsync(dto.Id);
        if (mdl == null)
            return GenResult<EmployeeDto>.NotFoundResult(JustTipMsgs.Error.NotFound<Employee>(dto.Id));

        mdl.Update(dto);

        var entity = await _repo.UpdateAsync(mdl);

        //await uow.SaveChangesAsync(cancellationToken);

        return GenResult<EmployeeDto>.Success(entity!.ToDto());

    }

}//Cls
