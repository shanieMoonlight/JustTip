namespace JustTip.Application.Features.Roster.Cmd.AddShift;


public record AddShiftToEmployeeCmd(ShiftDto Dto) : IJtCommand<ShiftDto>;
