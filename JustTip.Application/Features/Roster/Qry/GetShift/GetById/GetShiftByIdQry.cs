namespace JustTip.Application.Features.Roster.Qry.GetShift.GetById;
public record GetShiftByIdQry(Guid? Id) : IJtQuery<ShiftWithEmployeeDto>;