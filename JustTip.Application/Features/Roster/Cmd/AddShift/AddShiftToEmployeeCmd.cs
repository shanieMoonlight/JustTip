namespace JustTip.Application.Features.Roster.Cmd.AddShift;


public record AddShiftToEmployeeCmd(AddShiftDto Dto) : IJtCommand<ShiftDto>;
