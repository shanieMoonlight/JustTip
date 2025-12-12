using JustTip.Application.Features.Tips.Qry.GetCurrentWeekTotalTips;

namespace JustTip.Application.Tests.Features.Tips.GetWeekTotalTips;

public class GetCurrentWeekTotalTipsQryHandlerTests
{
    [Fact]
    public async Task CurrentHandler_CallsRepoWithLocalWeekConvertedToUtc()
    {
        var mockRepo = new Mock<ITipRepo>();
        var mockUtils = new Mock<IRosterUtils>();

        var handler = new GetCurrentWeekTotalTipsQryHandler(mockRepo.Object, mockUtils.Object);

        var now = new DateTime(2025,1,8,12,0,0, DateTimeKind.Utc);
        var weekStart = new DateTime(2025,1,6,0,0,0, DateTimeKind.Utc);
        var weekEnd = weekStart.AddDays(7);

        mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(weekStart);
        mockRepo.Setup(r => r.GetTotalTipsAsync(weekStart, weekEnd, It.IsAny<CancellationToken>())).ReturnsAsync(55.5m);

        var res = await handler.Handle(new GetCurrentWeekTotalTipsQry(), CancellationToken.None);

        res.ShouldBeOfType<GenResult<double>>();
        res.Value.ShouldBe((double)55.5);
        mockRepo.Verify(r => r.GetTotalTipsAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()), Times.Once);
    }
}
