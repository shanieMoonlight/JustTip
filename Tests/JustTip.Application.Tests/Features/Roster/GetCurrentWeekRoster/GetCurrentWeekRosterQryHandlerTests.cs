using JustTip.Application.Domain.Entities.Shifts;
using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Qry.GetCurrentWeekRoster;
using JustTip.Application.LocalServices.AppServices;

namespace JustTip.Application.Tests.Features.Roster.GetCurrentWeekRoster;

public class GetCurrentWeekRosterQryHandlerTests
{
    private readonly Mock<IShiftRepo> _mockRepo = new();
    private readonly Mock<IRosterUtils> _mockUtils = new();
    private readonly GetCurrentWeekRosterQryHandler _handler;

    public GetCurrentWeekRosterQryHandlerTests()
    {
        _handler = new GetCurrentWeekRosterQryHandler(_mockRepo.Object, _mockUtils.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallRepoWithWeekRangeAndReturnDto()
    {
        // Arrange
        var weekStart = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc); // Monday
        var weekEnd = weekStart.AddDays(7);

        _mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(weekStart);

        var empty = new List<Shift>();
        _mockRepo.Setup(r => r.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(empty);

        var expected = new RosterDto(weekStart, weekEnd, []);
        _mockUtils.Setup(u => u.ConvertToShiftRosterItemDto(empty, weekStart, weekEnd)).Returns(expected);

        // Act
        var result = await _handler.Handle(new GetCurrentWeekRosterQry(), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<RosterDto>>();
        result.Value.ShouldBe(expected);
        _mockRepo.Verify(r => r.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()), Times.Once);
        _mockUtils.Verify(u => u.ConvertToShiftRosterItemDto(empty, weekStart, weekEnd), Times.Once);
    }

}
