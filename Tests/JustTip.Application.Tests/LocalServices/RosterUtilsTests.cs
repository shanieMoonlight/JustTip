using JustTip.Application.LocalServices.Imps;

namespace JustTip.Application.Tests.LocalServices;

public class RosterUtilsTests
{
    [Fact]
    public void ToLocalTime_ShouldConvertManyUtcInputs()
    {
        var utils = new RosterUtils();

        var samples = new List<DateTime>
        {
            new(2020,1,1,0,0,0, DateTimeKind.Utc),
            new(2020,6,1,12,30,0, DateTimeKind.Utc),
            new(2021,3,28,1,30,0, DateTimeKind.Utc), // DST boundary area
            new(2021,11,7,5,0,0, DateTimeKind.Utc),
            new(2022,12,31,23,59,59, DateTimeKind.Utc),
            new(2023,4,15,9,15,0, DateTimeKind.Utc),
            new(2024,2,29,8,0,0, DateTimeKind.Utc), // leap year
            new(2025,7,4,18,45,0, DateTimeKind.Utc),
            new(2030,10,31,0,0,0, DateTimeKind.Utc),
            new(1999,12,31,23,0,0, DateTimeKind.Utc)
        };

        foreach (var utc in samples)
        {
            var expected = TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.Local);
            var actual = utils.ToLocalTime(utc);
            actual.ShouldBe(expected);

            // also ensure Unspecified is treated as UTC
            var unspecified = DateTime.SpecifyKind(utc, DateTimeKind.Unspecified);
            var actualUnspecified = utils.ToLocalTime(unspecified);
            actualUnspecified.ShouldBe(expected);
        }
    }

    //--------------------------//

    [Fact]
    public void GetMostRecentDay_ShouldBehaveCorrectlyAcrossManyCases()
    {
        var utils = new RosterUtils();

        var testDates = new List<DateTime>
        {
            new(2023,12,11), // Monday
            new(2023,12,12), // Tuesday
            new(2023,12,13), // Wednesday
            new(2023,12,14), // Thursday
            new(2023,12,15), // Friday
            new(2023,12,16), // Saturday
            new(2023,12,17), // Sunday
            new(2024,1,1),   // Monday
            new(2024,2,29),  // Thursday (leap)
            new(2025,7,4)    // Friday
        };

        var weekdays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };

        foreach (var now in testDates)
        {
            foreach (var target in weekdays)
            {
                var result = utils.GetMostRecentDay(now, target);

                // result must be the requested weekday
                result.DayOfWeek.ShouldBe(target);

                // result must not be in the future relative to now
                result.Date.ShouldBeLessThanOrEqualTo(now.Date);

                // difference must be less than 7 days
                (now.Date - result.Date).TotalDays.ShouldBeInRange(0, 6);
            }
        }
    }
}
