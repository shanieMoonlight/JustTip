namespace JustTip.Application.Features.Employees.Cmd.Shifts.AddShift;


public record AddShiftToEmployeeCmd(ShiftDto Dto) : IJtCommand<ShiftDto>;
