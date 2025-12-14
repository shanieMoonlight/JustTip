namespace JustTip.Application.Features.Roster.Cmd.UpdateShift;


public record UpdateShiftCmd(Guid Id, ShiftDto Dto) : IJtCommand<ShiftDto>;
