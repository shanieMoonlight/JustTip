using JustTip.Application.LocalServices.Imps;

namespace JustTip.Application.Tests.LocalServices;

public class RosterUtilsConvertTests
{
    [Fact]
    public void ConvertToShiftRosterItemDto_GroupsAndOrdersAndConvertsLocalTime()
    {
        // Arrange
        var rosterUtils = new RosterUtils();

        var empA = Employee.Create("Alice", "desc");
        var empB = Employee.Create("Bob", "desc");

        // pick a date in the near future
        var date1 = DateTime.UtcNow.Date.AddDays(1);
        var date2 = date1.AddDays(1);

        // empA shift at 09:00
        var shiftA = empA.AddShift(date1, date1.AddHours(9), date1.AddHours(17));
        // empB shift earlier at 08:00 same day
        var shiftB = empB.AddShift(date1, date1.AddHours(8), date1.AddHours(12));

        // empA another shift next day
        var shiftA2 = empA.AddShift(date2, date2.AddHours(10), date2.AddHours(14));

        var allShifts = new[] { shiftA, shiftB, shiftA2 };

        var weekStart = date1.AddDays(-1);
        var weekEnd = date2.AddDays(2);

        // Act
        var roster = rosterUtils.ConvertToShiftRosterItemDto(allShifts, weekStart, weekEnd);

        // Assert grouping: two days
        roster.Days.Count.ShouldBe(2);

        var day1 = roster.Days.First(d => d.Date == date1.Date);
        day1.Shifts.Count.ShouldBe(2);

        // ordering: earliest start first (shiftB at 08:00 then shiftA at 09:00)
        day1.Shifts[0].EmployeeName.ShouldBe("Bob");
        day1.Shifts[1].EmployeeName.ShouldBe("Alice");

        // local time conversion: start equals ToLocalTime of stored UTC StartTime
        var firstShift = day1.Shifts[0];
        firstShift.StartTime.ShouldBe(rosterUtils.ToLocalTime(shiftB.StartTimeUtc));
    }

    [Fact]
    public void ConvertToShiftRosterItemDto_TieOnStart_SortsByEmployeeName()
    {
        // Arrange
        var rosterUtils = new RosterUtils();

        var emp1 = Employee.Create("Charlie", "desc");
        var emp2 = Employee.Create("Adam", "desc");

        var date = DateTime.UtcNow.Date.AddDays(2);
        var start = date.AddHours(9);
        var end = date.AddHours(13);

        var s1 = emp1.AddShift(date, start, end);
        var s2 = emp2.AddShift(date, start, end);

        var all = new[] { s1, s2 };
        var roster = rosterUtils.ConvertToShiftRosterItemDto(all, date.AddDays(-1), date.AddDays(3));

        var day = roster.Days.Single(d => d.Date == date.Date);
        // Because names Adam < Charlie, Adam should appear first after sorting by name when start times tie
        day.Shifts[0].EmployeeName.ShouldBe("Adam");
        day.Shifts[1].EmployeeName.ShouldBe("Charlie");
    }

    [Fact]
    public void ConvertToShiftRosterItemDto_OrderPrecedence_DateThenStartThenName()
    {
        // Arrange
        var rosterUtils = new RosterUtils();

        var empX = Employee.Create("Xavier", "desc");
        var empY = Employee.Create("Yvonne", "desc");
        var empA = Employee.Create("Aaron", "desc");
        var empZ = Employee.Create("Zara", "desc");

        var date = DateTime.UtcNow.Date.AddDays(2);
        var dateNext = date.AddDays(1);

        // On same date, create shifts with different starts and names to validate ordering
        var s1 = empX.AddShift(date, date.AddHours(10), date.AddHours(14)); // start 10
        var s2 = empY.AddShift(date, date.AddHours(9), date.AddHours(13));  // start 9
        var s3 = empA.AddShift(date, date.AddHours(9), date.AddHours(12));  // start 9, name Aaron < Yvonne

        // Shift on a later date - should appear after the above date
        var s4 = empZ.AddShift(dateNext, dateNext.AddHours(8), dateNext.AddHours(12));

        var all = new[] { s1, s2, s3, s4 };

        // Act
        var roster = rosterUtils.ConvertToShiftRosterItemDto(all, date.AddDays(-1), date.AddDays(4));

        // Assert: days ordered (date then dateNext)
        roster.Days.Count.ShouldBe(2);
        roster.Days[0].Date.ShouldBe(date.Date);
        roster.Days[1].Date.ShouldBe(dateNext.Date);

        var day = roster.Days[0];
        // Within the same day: start 9 entries first, ordered by employee name (Aaron then Yvonne), then start 10 (Xavier)
        day.Shifts.Count.ShouldBe(3);
        day.Shifts[0].EmployeeName.ShouldBe("Aaron");
        day.Shifts[1].EmployeeName.ShouldBe("Yvonne");
        day.Shifts[2].EmployeeName.ShouldBe("Xavier");
    }
}
