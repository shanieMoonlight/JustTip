namespace JustTip.Application.Features.Employees.Cmd.Shifts.RemoveShift;
public class RemoveShiftFromEmployeeCmdHandler(IEmployeeRepo _repo) : IJtCommandHandler<RemoveShiftFromEmployeeCmd, EmployeeDto>
{
    public async Task<GenResult<EmployeeDto>> Handle(RemoveShiftFromEmployeeCmd request, CancellationToken cancellationToken)
    {

        var dto = request.Dto;

        var employee = await _repo.FirstOrDefaultByIdWithShiftsAsync(dto.EmployeeId);

        if (employee is null)
            return GenResult<EmployeeDto>.NotFoundResult(JustTipMsgs.Error.NotFound<Employee>(dto.EmployeeId));


       employee.RemoveShift(dto.ShiftId);

        return GenResult<EmployeeDto>.Success(employee.ToDto());

    }


}//Cls
