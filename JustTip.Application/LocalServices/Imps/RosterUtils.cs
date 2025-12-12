using JustTip.Application.Features.Roster;
using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.LocalServices.Imps;

public class RosterUtils : IRosterUtils
{
    //Could inject some sort of time zone provider here if needed.
    private readonly TimeZoneInfo _timeZone = TimeZoneInfo.Local;

    public DateTime ToLocalTime(DateTime utc)
    {
        if (utc.Kind != DateTimeKind.Utc)
            utc = DateTime.SpecifyKind(utc, DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeFromUtc(utc, _timeZone);
    }

    //--------------------------//

    public DateTime GetMostRecentDay(DateTime now, DayOfWeek dayOfWeek)
    {
        var dt = now.Date;
        int diff = ((int)dt.DayOfWeek - (int)dayOfWeek + 7) % 7;
        return dt.AddDays(-diff);
    }


    //--------------------------//


    public RosterDto ConvertToShiftRosterItemDto(IEnumerable<Shift> shifts, DateTime weekStart, DateTime weekEnd)
    {
        var items = shifts
            .OrderBy(s => s.Date)
                .ThenBy(s => s.StartTimeUtc)
                .ThenBy(s => s.Employee!.Name)
            .Select(s => new ShiftRosterItemDto(
                ShiftId: s.Id,
                EmployeeId: s.EmployeeId,
                EmployeeName: s.Employee!.Name, //! trust the repo to load employee
                Date: s.Date.Date,
                StartTime: ToLocalTime(s.StartTimeUtc),
                EndTime: ToLocalTime(s.EndTimeUtc)
            )).ToList();


        var days = items
            .GroupBy(i => i.Date)
            .OrderBy(g => g.Key)
            .Select(g => new DayRosterDto(g.Key, [.. g.OrderBy(i => i.StartTime)]))
            .ToList();


        return new RosterDto(ToLocalTime(weekStart), ToLocalTime(weekEnd), days);
    }
}//Cls
