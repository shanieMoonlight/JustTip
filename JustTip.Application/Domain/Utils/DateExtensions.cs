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

    public static TimeSpan RoundToNearestMinute(this TimeSpan ts, int minutes)
    {
        TimeSpan interval = TimeSpan.FromMinutes(Math.Abs(minutes));
        long intervalTicks = interval.Ticks;
        long half = intervalTicks / 2;
        long roundedTicks = ((ts.Ticks + half) / intervalTicks) * intervalTicks;
        return TimeSpan.FromTicks(roundedTicks);
    }
}


