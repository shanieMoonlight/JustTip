namespace JustTip.Application.Features.Employees.Cmd.Shifts.RemoveShift;

public record RemoveShiftDto(Guid EmployeeId, Guid ShiftId  );
public record RemoveShiftFromEmployeeCmd(RemoveShiftDto Dto) : IJtCommand<EmployeeDto>;
