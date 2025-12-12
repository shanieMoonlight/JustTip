using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CurrentRoster = JustTip.Application.Features.Roster.Qry.GetUpcomingWeekRoster;
using JustTip.Application.Features.Roster.Qry.GetUpcomingWeekRoster;
using JustTip.Application.LocalServices.AppServices;
using JustTip.Application.Domain.Entities.Shifts;
using JustTip.Application.Domain.Entities.Shifts;
using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Qry.GetCurrentWeekRoster;
using JustTip.Application.LocalServices.AppServices;
using Moq;
using Shouldly;

namespace JustTip.Application.Tests.Features.Roster.GetUpcomingWeekRoster;

public class GetUpcomingWeekRosterHandlerTests
{
    private readonly Mock<IShiftRepo> _mockRepo = new();
    private readonly Mock<IRosterUtils> _mockUtils = new();
    private readonly GetUpcomingWeekRosterHandler _handler;

    public GetUpcomingWeekRosterHandlerTests()
    {
        _handler = new GetUpcomingWeekRosterHandler(_mockRepo.Object, _mockUtils.Object);
    }

    [Fact]
    public async Task Handle_ShouldQueryRepoWithUtcDayAlignedRange_AndReturnDto()
    {
        // Arrange
        var weekStart = DateTime.UtcNow.Date;
        var weekEnd = weekStart.AddDays(7);

        var shifts = new List<Shift>();
        _mockRepo.Setup(r => r.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(shifts);

        var expected = new RosterDto(weekStart, weekEnd, []);
        _mockUtils.Setup(u => u.ConvertToShiftRosterItemDto(shifts, weekStart, weekEnd)).Returns(expected);

        // Act
        var result = await _handler.Handle(new GetUpcomingWeekRosterQry(), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<RosterDto>>();
        result.Value.ShouldBe(expected);
        _mockRepo.Verify(r => r.ListByDateRangeWithEmployeeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()), Times.Once);
        _mockUtils.Verify(u => u.ConvertToShiftRosterItemDto(shifts, weekStart, weekEnd), Times.Once);
    }
}
