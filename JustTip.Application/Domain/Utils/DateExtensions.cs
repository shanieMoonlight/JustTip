namespace JustTip.Application.Domain.Utils;
public static class DateExtensions
{
    public static DateTime RoundToNearestMinute(this DateTime dt, int minutes)
    {
        TimeSpan interval = TimeSpan.FromMinutes(Math.Abs(minutes));
        long intervalTicks = interval.Ticks;
        long half = intervalTicks / 2;
        long roundedTicks = ((dt.Ticks + half) / intervalTicks) * intervalTicks;
        // preserve Kind
        return new DateTime(roundedTicks, dt.Kind);
    }

    //----------------------//

    public static TimeSpan RoundToNearestMinute(this TimeSpan ts, int minutes)
    {
        TimeSpan interval = TimeSpan.FromMinutes(Math.Abs(minutes));
        long intervalTicks = interval.Ticks;
        long half = intervalTicks / 2;
        long roundedTicks = ((ts.Ticks + half) / intervalTicks) * intervalTicks;
        return TimeSpan.FromTicks(roundedTicks);
    }

    //----------------------//

    public static DateTime ToUtcDate(this DateTime dt) =>
        dt.Kind == DateTimeKind.Utc
            ? dt
            : TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dt, DateTimeKind.Unspecified), TimeZoneInfo.Local);

    //----------------------//

    /// <summary>
    /// Sum clipped seconds for a collection of (start,end) pairs against a given range [rangeStart, rangeEnd).
    /// Each pair will be clipped to the provided range before summing. Returns total seconds as a long.
    /// </summary>
    public static long SumClippedSeconds(this IEnumerable<(DateTime Start, DateTime End)> shifts, DateTime rangeStart, DateTime rangeEnd)
    {
        long totalSeconds = 0;
        var startTicks = rangeStart.Ticks;
        var endTicks = rangeEnd.Ticks;

        foreach (var (Start, End) in shifts)
        {
            var clippedStartTicks = Math.Max(Start.Ticks, startTicks);
            var clippedEndTicks = Math.Min(End.Ticks, endTicks);
            var deltaTicks = clippedEndTicks - clippedStartTicks;
            if (deltaTicks <= 0) continue;

            var seconds = (long)TimeSpan.FromTicks(deltaTicks).TotalSeconds;
            totalSeconds += seconds;
        }

        return totalSeconds;
    }

    //----------------------//
}


