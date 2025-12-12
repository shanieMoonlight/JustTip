using JustTip.Application.Features.Roster;

namespace JustTip.Application.LocalServices.AppServices;

/// <summary>
/// Small utility helpers for roster handling and timezone conversions.
/// Designed to be simple and deterministic for tests in the demo app.
/// </summary>
public interface IRosterUtils
{
    /// <summary>
    /// Convert a UTC <see cref="DateTime"/> to the configured local time zone.
    /// The input will be treated as UTC even if its Kind is Unspecified.
    /// </summary>
    DateTime ToLocalTime(DateTime utc);

    /// <summary>
    /// Returns the date (midnight) of the most recent occurrence of <paramref name="dayOfWeek"/>
    /// relative to <paramref name="now"/>. If <paramref name="now"/> falls on the requested day,
    /// that day's date is returned.
    /// </summary>
    DateTime GetMostRecentDay(DateTime now, DayOfWeek dayOfWeek);

    /// <summary>
    /// Convenience: most recent Monday for the provided <paramref name="now"/>.
    /// </summary>
    DateTime GetMostRecentMonday(DateTime now) => GetMostRecentDay(now, DayOfWeek.Monday);


    RosterDto ConvertToShiftRosterItemDto(IEnumerable<Shift> shifts, DateTime weekStart, DateTime weekEnd);
}
