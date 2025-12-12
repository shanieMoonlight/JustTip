namespace JustTip.Application.Tests.Domain.Utils;

public class SumClippedSecondsTests
{
    [Fact]
    public void SingleShiftFullyInsideRange_ReturnsFullShiftSeconds()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var shiftStart = rangeStart.AddDays(1).AddHours(9);
        var shiftEnd = shiftStart.AddHours(8); // 8 hours

        var shifts = new List<(DateTime Start, DateTime End)> { (shiftStart, shiftEnd) };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);

        total.ShouldBe((long)TimeSpan.FromHours(8).TotalSeconds);
    }

    //----------------------//

    [Fact]
    public void ShiftStartsBeforeRange_ClipsAtRangeStart()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var shiftStart = rangeStart.AddDays(-1).AddHours(20); // previous day 20:00
        var shiftEnd = rangeStart.AddHours(4); // overlaps first 4 hours of range

        var shifts = new List<(DateTime Start, DateTime End)> { (shiftStart, shiftEnd) };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);

        total.ShouldBe((long)TimeSpan.FromHours(4).TotalSeconds);
    }

    //----------------------//

    [Fact]
    public void ShiftEndsAfterRange_ClipsAtRangeEnd()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var shiftStart = rangeEnd.AddHours(-5); // 5 hours before range end
        var shiftEnd = rangeEnd.AddHours(10); // ends after range

        var shifts = new List<(DateTime Start, DateTime End)> { (shiftStart, shiftEnd) };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);

        total.ShouldBe((long)TimeSpan.FromHours(5).TotalSeconds);
    }

    //----------------------//

    [Fact]
    public void ShiftOutsideRange_ReturnsZero()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var beforeStart = rangeStart.AddDays(-2);
        var beforeEnd = rangeStart.AddDays(-1);

        var afterStart = rangeEnd.AddDays(1);
        var afterEnd = rangeEnd.AddDays(2);

        var shifts = new List<(DateTime Start, DateTime End)>
        {
            (beforeStart, beforeEnd),
            (afterStart, afterEnd)
        };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);
        total.ShouldBe(0);
    }

    //----------------------//

    [Fact]
    public void MultipleShifts_SumsClippedSeconds()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var s1 = (rangeStart.AddHours(8), rangeStart.AddHours(12)); // 4h
        var s2 = (rangeStart.AddDays(2).AddHours(9), rangeStart.AddDays(2).AddHours(17)); // 8h
        var s3 = (rangeEnd.AddHours(-2), rangeEnd.AddHours(3)); // clipped to 2h

        var shifts = new List<(DateTime Start, DateTime End)> { s1, s2, s3 };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);

        var expected = (long)(TimeSpan.FromHours(4).TotalSeconds + TimeSpan.FromHours(8).TotalSeconds + TimeSpan.FromHours(2).TotalSeconds);
        total.ShouldBe(expected);
    }

    //----------------------//

    [Fact]
    public void EdgeTouchingShift_EndEqualsRangeStart_IsExcluded()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var shift = (rangeStart.AddHours(-3), rangeStart); // ends exactly at rangeStart
        var shifts = new List<(DateTime Start, DateTime End)> { shift };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);
        total.ShouldBe(0);
    }

    //----------------------//

    [Fact]
    public void EdgeTouchingShift_StartEqualsRangeEnd_IsExcluded()
    {
        var rangeStart = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var rangeEnd = rangeStart.AddDays(7);

        var shift = (rangeEnd, rangeEnd.AddHours(4)); // starts exactly at rangeEnd
        var shifts = new List<(DateTime Start, DateTime End)> { shift };

        var total = shifts.SumClippedSeconds(rangeStart, rangeEnd);
        total.ShouldBe(0);
    }
}
