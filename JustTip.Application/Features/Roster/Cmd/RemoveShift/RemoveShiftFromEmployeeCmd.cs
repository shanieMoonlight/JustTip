namespace JustTip.Application.Features.Roster.Cmd.RemoveShift;

//public record RemoveShiftDto(Guid EmployeeId, Guid ShiftId  );
public record RemovedResult(Guid Id);
public record RemoveShiftFromEmployeeCmd(Guid EmployeeId, Guid ShiftId) : IJtCommand<RemovedResult>;
