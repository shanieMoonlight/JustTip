namespace JustTip.Application.Features.Roster.Cmd.RemoveShift;
public class RemoveShiftFromEmployeeCmdHandler(IEmployeeRepo _repo) : IJtCommandHandler<RemoveShiftFromEmployeeCmd, RemovedResult>
{
    public async Task<GenResult<RemovedResult>> Handle(RemoveShiftFromEmployeeCmd request, CancellationToken cancellationToken)
    {

        var employeeId = request.EmployeeId;
        var shiftId = request.ShiftId;

        var employee = await _repo.FirstOrDefaultByIdWithShiftsAsync(employeeId);

        if (employee is null)
            return GenResult<RemovedResult>.NotFoundResult(JustTipMsgs.Error.NotFound<Employee>(employeeId));


       employee.RemoveShift(shiftId);

        var result = new RemovedResult(shiftId);
        return GenResult<RemovedResult>.Success((result));

        //return GenResult<EmployeeDto>.Success(employee.ToDto());

    }


}//Cls
