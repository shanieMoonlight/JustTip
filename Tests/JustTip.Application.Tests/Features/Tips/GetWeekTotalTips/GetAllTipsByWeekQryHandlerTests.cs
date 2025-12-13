//using JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;

using JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;

namespace JustTip.Application.Tests.Features.Tips.GetWeekTotalTips;

public class GetAllTipsByWeekQryHandlerTests
{
    [Fact]
    public async Task Handle_WeekNumber0_UsesCurrentWeek()
    {
        var mockRepo = new Mock<ITipRepo>();
        var handler = new GetAllTipsByWeekQryHandler(mockRepo.Object);

        var now = DateTime.UtcNow;
        var weekStart = now.Date.AddDays(-(((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7));
        var weekEnd = weekStart.AddDays(7);

        var tip = Tip.Create(DateTime.UtcNow, 12.34m);
        var tips = new List<Tip> { tip };
        mockRepo.Setup(r => r.ListByDateRangeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>())).ReturnsAsync((IReadOnlyList<Tip>)tips);

        var res = await handler.Handle(new GetAllTipsByWeekQry(0), CancellationToken.None);

        res.ShouldBeOfType<GenResult<List<TipDto>>>();
        res.Value!.Count.ShouldBe(1);
        mockRepo.Verify(r => r.ListByDateRangeAsync(weekStart, weekEnd, It.IsAny<CancellationToken>()), Times.Once);
    }



    [Fact]
    public async Task Handle_WeekNumber1_UsesLastWeek()
    {
        var mockRepo = new Mock<ITipRepo>();
        var handler = new GetAllTipsByWeekQryHandler(mockRepo.Object);

        var now = DateTime.UtcNow;
        var thisMonday = now.Date.AddDays(-(((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7));
        var lastMonday = thisMonday.AddDays(-7);
        var lastMondayEnd = lastMonday.AddDays(7);

        var tip = Tip.Create(lastMonday.AddDays(1), 5m);
        var tips = new List<Tip> { tip };
        mockRepo.Setup(r => r.ListByDateRangeAsync(lastMonday, lastMondayEnd, It.IsAny<CancellationToken>())).ReturnsAsync((IReadOnlyList<Tip>)tips);

        var res = await handler.Handle(new GetAllTipsByWeekQry(1), CancellationToken.None);

        res.Value!.Count.ShouldBe(1);
        mockRepo.Verify(r => r.ListByDateRangeAsync(lastMonday, lastMondayEnd, It.IsAny<CancellationToken>()), Times.Once);
    }
}
