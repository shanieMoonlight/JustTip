using JustTip.Application.Domain.Utils;

namespace JustTip.Application.Tests.Domain.Utils;

public class DateExtensionsTests
{
    [Fact]
    public void RoundToNearestMinute_DateTime_UsesKnownExpectedResults()
    {
        var cases = new (DateTime input, int minutes, DateTime expected)[]
        {
            // 30s -> rounds up to 00:01
            (new DateTime(2023,1,1,0,0,30, DateTimeKind.Utc), 1, new DateTime(2023,1,1,0,1,0, DateTimeKind.Utc)),
            // 29s -> rounds down to 00:02
            (new DateTime(2023,1,1,0,2,29, DateTimeKind.Utc), 1, new DateTime(2023,1,1,0,2,0, DateTimeKind.Utc)),
            // 12:07:31 rounded to nearest 5 -> 12:10
            (new DateTime(2023,1,1,12,7,31, DateTimeKind.Utc), 5, new DateTime(2023,1,1,12,10,0, DateTimeKind.Utc)),
            // 23:58:59 rounded to nearest 15 -> next day 00:00
            (new DateTime(2023,6,15,23,58,59, DateTimeKind.Utc), 15, new DateTime(2023,6,16,0,0,0, DateTimeKind.Utc)),
            // 03:14:15 rounded to nearest 30 -> 03:00
            (new DateTime(2024,2,29,3,14,15, DateTimeKind.Utc), 30, new DateTime(2024,2,29,3,0,0, DateTimeKind.Utc)),
            // 23:59:29 rounds down
            (new DateTime(2025,12,31,23,59,29, DateTimeKind.Utc), 1, new DateTime(2025,12,31,23,59,0, DateTimeKind.Utc)),
            // 06:33:31 rounded to nearest 2 -> 06:34
            (new DateTime(2000,2,29,6,33,31, DateTimeKind.Utc), 2, new DateTime(2000,2,29,6,34,0, DateTimeKind.Utc)),
            // unspecified kind preserved and rounded to nearest 3 -> 11:12 unspecified
            (new DateTime(1999,12,31,11,11,11, DateTimeKind.Unspecified), 3, new DateTime(1999,12,31,11,12,0, DateTimeKind.Unspecified)),
            // local kind preserved, 08:08:08 rounded to nearest 7 -> 08:08 local (closer to 08:08 than 08:07)
            (new DateTime(2010,7,4,8,8,8, DateTimeKind.Local), 7, new DateTime(2010,7,4,8,8,0, DateTimeKind.Local)),
            // tie case: 02:30 with interval 12 -> rounds up to 02:36
            (new DateTime(2022,3,13,2,30,0, DateTimeKind.Utc), 12, new DateTime(2022,3,13,2,36,0, DateTimeKind.Utc))
        };
        foreach (var (input, minutes, expected) in cases)
        {
            var actual = input.RoundToNearestMinute(minutes);
            actual.ShouldBe(expected);
            actual.Kind.ShouldBe(input.Kind);
        }
    }

    [Fact]
    public void RoundToNearestMinute_TimeSpan_UsesKnownExpectedResults()
    {
        var cases = new (TimeSpan input, int minutes, TimeSpan expected)[]
        {
            (TimeSpan.FromSeconds(30), 1, TimeSpan.FromMinutes(1)),
            (new TimeSpan(0,2,29), 1, new TimeSpan(0,2,0)),
            (new TimeSpan(12,7,31), 5, new TimeSpan(12,10,0)),
            (new TimeSpan(23,58,59), 15, TimeSpan.FromDays(1)), // rounds up to next day (24:00)
            (new TimeSpan(3,14,15), 30, new TimeSpan(3,0,0)),
            (new TimeSpan(23,59,29), 1, new TimeSpan(23,59,0)),
            (new TimeSpan(6,33,31), 2, new TimeSpan(6,34,0)),
            (new TimeSpan(11,11,11), 3, new TimeSpan(11,12,0)),
            (new TimeSpan(8,8,8), 7, new TimeSpan(8,10,0)),
            (new TimeSpan(2,30,0), 12, new TimeSpan(2,36,0))
        };

        foreach (var (input, minutes, expected) in cases)
        {
            var actual = input.RoundToNearestMinute(minutes);
            actual.ShouldBe(expected);
        }
    }

}
