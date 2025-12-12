using JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;

namespace JustTip.Application.Tests.Features.Tips.GetWeekTotalTips;

public class GetWeekTotalTipsHandlerTests
{
    [Fact]
    public async Task UpcomingHandler_CallsRepoWithUtcDayAlignedRange()
    {
        var mockRepo = new Mock<ITipRepo>();
        var handler = new GetUpcomingWeekTotalTipsQryHandler(mockRepo.Object);

        var weekStart = DateTime.UtcNow.Date;
        var weekEnd = weekStart.AddDays(7);

        mockRepo.Setup(r => r.GetTotalTipsAsync(weekStart, weekEnd, It.IsAny<CancellationToken>())).ReturnsAsync(123.45m);

        var res = await handler.Handle(new GetUpcomingWeekTotalTipsQry(), CancellationToken.None);

        res.ShouldBeOfType<GenResult<double>>();
        res.Value.ShouldBe((double)123.45);
        mockRepo.Verify(r => r.GetTotalTipsAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()), Times.Once);
    }
}
