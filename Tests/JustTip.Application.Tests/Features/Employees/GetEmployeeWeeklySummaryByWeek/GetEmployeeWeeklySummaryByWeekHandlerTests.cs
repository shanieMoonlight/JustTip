using JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummaryByWeek;
using JustTip.Application.LocalServices.Abs;
using System.Diagnostics;

namespace JustTip.Application.Tests.Features.Employees.GetEmployeeWeeklySummaryByWeek;

public class GetEmployeeWeeklySummaryByWeekHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockEmpRepo;
    private readonly Mock<IShiftRepo> _mockShiftRepo;
    private readonly Mock<ITipRepo> _mockTipRepo;
    private readonly Mock<IRosterUtils> _mockUtils;
    private readonly Mock<ITipCalculatorService> _mockCalc;

    //- - - - - - - - - - - - - //

    public GetEmployeeWeeklySummaryByWeekHandlerTests()
    {
        _mockEmpRepo = new Mock<IEmployeeRepo>();
        _mockShiftRepo = new Mock<IShiftRepo>();
        _mockTipRepo = new Mock<ITipRepo>();
        _mockUtils = new Mock<IRosterUtils>();
        _mockCalc = new Mock<ITipCalculatorService>();
    }

    //--------------------------// 


    [Fact]
    public async Task Handle_ReturnsNotFound_WhenEmployeeMissing()
    {
        var handler = new GetEmployeeWeeklySummaryByWeekHandler(
            _mockEmpRepo.Object, _mockShiftRepo.Object, _mockTipRepo.Object, _mockUtils.Object, _mockCalc.Object);

        _mockUtils.Setup(_mockShiftRepo => _mockShiftRepo.GetMostRecentMonday(It.IsAny<DateTime>()))
            .Returns(DateTime.UtcNow.Date);
        _mockEmpRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Employee?)null);

        var res = await handler.Handle(new GetEmployeeWeeklySummaryByWeekQry(Guid.NewGuid(), 1), CancellationToken.None);

        res.NotFound.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_ComputesSummary_WhenEmployeeFound()
    {

        var employee = Employee.Create("Sam", "desc");
        _mockEmpRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(employee.Id)).ReturnsAsync(employee);

        var thisMonday = new DateTime(2025,12,1,0,0,0, DateTimeKind.Utc);
        var days = 1;
        var rangeStartDate = thisMonday.AddDays(-7 * days);
        var rangeEndDate = rangeStartDate.AddDays(7);


        Debug.WriteLine($"rangeStartDate:{rangeStartDate:f}");
        Debug.WriteLine($"rangeEndDate:{rangeEndDate:f}");

        _mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>()))
            .Returns(thisMonday);
        _mockUtils.Setup(u => u.ToLocalTime(rangeStartDate))
            .Returns(rangeStartDate.ToLocalTime());
        _mockUtils.Setup(u => u.ToLocalTime(rangeEndDate))
            .Returns(rangeEndDate.ToLocalTime());

        _mockShiftRepo.Setup(r => r.GetTotalSecondsForEmployeeInRangeAsync(employee.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(2*3600);
        _mockShiftRepo.Setup(r => r.GetTotalSecondsAllInRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(8*3600);
        _mockTipRepo.Setup(r => r.GetTotalTipsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(100m);

        _mockCalc.Setup(c => c.Calculate(2*3600, 8*3600, 100m)).Returns(new TipSplit(2.0, 25.0m));

        var handler = new GetEmployeeWeeklySummaryByWeekHandler(
            _mockEmpRepo.Object, _mockShiftRepo.Object, _mockTipRepo.Object, _mockUtils.Object, _mockCalc.Object);
        var qry = new GetEmployeeWeeklySummaryByWeekQry(employee.Id, 1);
        var res = await handler.Handle(qry, CancellationToken.None);

        Debug.WriteLine($"res.Value!.RangeStartDate:{res.Value!.RangeStartDate:f}");
        Debug.WriteLine($"res.Value!.RangeEndDate:{res.Value!.RangeEndDate:f}");


        res.Succeeded.ShouldBeTrue();
        res.Value!.EmployeeId.ShouldBe(employee.Id);
        res.Value!.HoursWorked.ShouldBe(2.0);
        res.Value!.TipShare.ShouldBe(25.0);
        res.Value!.RangeEndDate.ShouldBe(rangeEndDate);
        res.Value!.RangeStartDate.ShouldBe(rangeStartDate);
    }
}
