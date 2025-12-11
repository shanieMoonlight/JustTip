using JustTip.Application.Features.Employees.Cmd;
using JustTip.Application.Features.Shifts.Cmd;

namespace JustTip.Application.Features.Employees.Cmd.Shifts.AddShift;
public class AddShiftToEmployeeCmdHandler(IEmployeeRepo _repo) : IJtCommandHandler<AddShiftToEmployeeCmd, ShiftDto>
{
    public async Task<GenResult<ShiftDto>> Handle(AddShiftToEmployeeCmd request, CancellationToken cancellationToken)
    {

        var dto = request.Dto;

        var employee = await _repo.FirstOrDefaultByIdAsync(dto.EmployeeId);

        if (employee is null)
            return GenResult<ShiftDto>.NotFoundResult(JtMsgs.Error.NotFound<Employee>(dto.EmployeeId));


       var shift =  employee.AddShift(dto.Date, dto.StartTimeUtc, dto.EndTimeUtc);

        return GenResult<ShiftDto>.Success(shift.ToDto());

    }


}//Cls
