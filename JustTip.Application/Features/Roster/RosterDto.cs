namespace JustTip.Application.Features.Roster;


public record ShiftRosterItemDto(
    Guid ShiftId,
    Guid EmployeeId,
    string EmployeeName,
    DateTime Date,
    DateTime StartTime,
    DateTime EndTime);

public record DayRosterDto(
    DateTime Date,
    IReadOnlyList<ShiftRosterItemDto> Shifts);

public record RosterDto(
    DateTime RangeStart,
    DateTime RangeEnd,
    IReadOnlyList<DayRosterDto> Days);
