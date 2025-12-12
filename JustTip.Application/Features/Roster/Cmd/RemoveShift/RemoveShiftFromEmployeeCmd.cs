using JustTip.Application.Features.Employees;

namespace JustTip.Application.Features.Roster.Cmd.RemoveShift;

public record RemoveShiftDto(Guid EmployeeId, Guid ShiftId  );
public record RemoveShiftFromEmployeeCmd(RemoveShiftDto Dto) : IJtCommand<EmployeeDto>;
