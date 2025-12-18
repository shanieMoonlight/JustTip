using JustTip.Application.Features.Tips.Qry.GetTipsTotalByWeek;

namespace JustTip.Application.Tests.Features.Tips.GetTipsTotalByWeek;

public class GetTipsTotalByWeekHandlerTests
{
    [Fact]
    public async Task Handle_WeekNumberNull_UsesThisWeek()
    {
        var mockRepo = new Mock<ITipRepo>();
        var mockUtils = new Mock<IRosterUtils>();

        var handler = new GetTipsTotalByWeekHandler(mockRepo.Object, mockUtils.Object);

        var thisMonday = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc);
        mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(thisMonday);

        var targetStart = thisMonday;
        var targetEnd = targetStart.AddDays(7);

        mockRepo.Setup(r => r.GetTotalTipsAsync(targetStart, targetEnd, It.IsAny<CancellationToken>())).ReturnsAsync(200.5m);

        var res = await handler.Handle(new GetTipsTotalByWeekQry(null), CancellationToken.None);

        res.ShouldBeOfType<GenResult<double>>();
        res.Succeeded.ShouldBeTrue();
        res.Value.ShouldBe((double)200.5m);
        mockRepo.Verify(r => r.GetTotalTipsAsync(targetStart, targetEnd, It.IsAny<CancellationToken>()), Times.Once);
        mockUtils.Verify(u => u.GetMostRecentMonday(It.IsAny<DateTime>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WeekNumber1_UsesPreviousWeek()
    {
        var mockRepo = new Mock<ITipRepo>();
        var mockUtils = new Mock<IRosterUtils>();

        var handler = new GetTipsTotalByWeekHandler(mockRepo.Object, mockUtils.Object);

        var thisMonday = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc);
        mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(thisMonday);

        var targetStart = thisMonday.AddDays(-7);
        var targetEnd = targetStart.AddDays(7);

        mockRepo.Setup(r => r.GetTotalTipsAsync(targetStart, targetEnd, It.IsAny<CancellationToken>())).ReturnsAsync(99.99m);

        var res = await handler.Handle(new GetTipsTotalByWeekQry(1), CancellationToken.None);

        res.Succeeded.ShouldBeTrue();
        res.Value.ShouldBe((double)99.99m);
        mockRepo.Verify(r => r.GetTotalTipsAsync(targetStart, targetEnd, It.IsAny<CancellationToken>()), Times.Once);
    }
}
