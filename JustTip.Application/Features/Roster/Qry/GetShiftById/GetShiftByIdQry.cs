namespace JustTip.Application.Features.Roster.Qry.GetShiftById;
public record GetShiftByIdQry(Guid? Id) : IJtQuery<ShiftWithEmployeeDto>;