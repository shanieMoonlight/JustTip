using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Qry.GetRosterByWeek;

namespace JustTip.Application.Tests.Features.Roster.GetRosterByWeek;

public class GetRosterByWeekHandlerTests
{
    private readonly Mock<IShiftRepo> _mockRepo = new();
    private readonly Mock<IRosterUtils> _mockUtils = new();
    private readonly GetRosterByWeekHandler _handler;

    public GetRosterByWeekHandlerTests()
    {
        _handler = new GetRosterByWeekHandler(_mockRepo.Object, _mockUtils.Object);
    }

    [Fact]
    public async Task Handle_WithWeekNumber0_UsesThisWeek()
    {
        // Arrange
        var thisMonday = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc);
        var weekStart = thisMonday;
        var weekEnd = weekStart.AddDays(7);

        _mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(thisMonday);

        var shifts = new List<Shift>();
        _mockRepo.Setup(r => r.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(shifts);

        var expected = new RosterDto(weekStart, weekEnd, []);
        _mockUtils.Setup(u => u.ConvertToShiftRosterItemDto(shifts, weekStart, weekEnd)).Returns(expected);

        // Act
        var result = await _handler.Handle(new GetRosterByWeekQry(0), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<RosterDto>>();
        result.Value.ShouldBe(expected);
        _mockRepo.Verify(r => r.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()), Times.Once);
        _mockUtils.Verify(u => u.ConvertToShiftRosterItemDto(shifts, weekStart, weekEnd), Times.Once);
    }

    //---------------------//

    [Fact]
    public async Task Handle_WithWeekNumber1_UsesNextWeek()
    {
        // Arrange
        var thisMonday = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc);
        var targetStart = thisMonday.AddDays(7);
        var targetEnd = targetStart.AddDays(7);

        _mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(thisMonday);

        var shifts = new List<Shift>();
        _mockRepo.Setup(r => r.ListByDateRangeWithEmployeeAsync(targetStart, targetEnd, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(shifts);

        var expected = new RosterDto(targetStart, targetEnd, []);
        _mockUtils.Setup(u => u.ConvertToShiftRosterItemDto(shifts, targetStart, targetEnd)).Returns(expected);

        // Act
        var result = await _handler.Handle(new GetRosterByWeekQry(1), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<RosterDto>>();
        result.Value.ShouldBe(expected);
        _mockRepo.Verify(r => r.ListByDateRangeWithEmployeeAsync(targetStart, targetEnd, It.IsAny<CancellationToken>()), Times.Once);
        _mockUtils.Verify(u => u.ConvertToShiftRosterItemDto(shifts, targetStart, targetEnd), Times.Once);
    }
}
