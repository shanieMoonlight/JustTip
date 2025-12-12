using JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummary;
using JustTip.Application.LocalServices.Abs;

namespace JustTip.Application.Tests.Features.Employees.GetEmployeesWeeklySummary;

public class GetEmployeesWeeklySummaryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenEmployeeMissing()
    {
        var empId = Guid.NewGuid();

        var mockEmployeeRepo = new Mock<IEmployeeRepo>();
        var mockShiftRepo = new Mock<IShiftRepo>();
        var mockTipRepo = new Mock<ITipRepo>();
        var mockUtils = new Mock<IRosterUtils>();

        mockEmployeeRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(empId)).ReturnsAsync((Employee?)null);

        var mockCalc = new Mock<ITipCalculatorService>();
        var handler = new GetEmployeeWeeklySummaryQryHandler(mockEmployeeRepo.Object, mockShiftRepo.Object, mockTipRepo.Object, mockUtils.Object, mockCalc.Object);

        var res = await handler.Handle(new GetEmployeeWeeklySummaryQry(empId), CancellationToken.None);

        res.NotFound.ShouldBeTrue();
        mockShiftRepo.VerifyNoOtherCalls();
        mockTipRepo.VerifyNoOtherCalls();
    }

    //----------------------//

    [Fact]
    public async Task Handle_ShouldComputeHoursAndTipShare()
    {
        var emp = Employee.Create("Sam", "desc");
        var empId = emp.Id;

        var mockEmployeeRepo = new Mock<IEmployeeRepo>();
        var mockShiftRepo = new Mock<IShiftRepo>();
        var mockTipRepo = new Mock<ITipRepo>();
        var mockUtils = new Mock<IRosterUtils>();

        // return employee
        mockEmployeeRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(empId)).ReturnsAsync(emp);

        // set week start
        var weekStart = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc);
        mockUtils.Setup(u => u.GetMostRecentMonday(It.IsAny<DateTime>())).Returns(weekStart);

        // employee seconds = 2 hours, total seconds = 8 hours -> tip share = 100*(2/8) = 25
        mockShiftRepo.Setup(r => r.GetTotalSecondsForEmployeeInRangeAsync(empId, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>())).ReturnsAsync(2 * 3600);
        mockShiftRepo.Setup(r => r.GetTotalSecondsAllInRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>())).ReturnsAsync(8 * 3600);
        mockTipRepo.Setup(r => r.GetTotalTipsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>())).ReturnsAsync(100m);

        var mockCalc = new Mock<ITipCalculatorService>();
        mockCalc.Setup(c => c.Calculate(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>())).Returns(new TipSplit(2.0, 25.0m));
        var handler = new GetEmployeeWeeklySummaryQryHandler(mockEmployeeRepo.Object, mockShiftRepo.Object, mockTipRepo.Object, mockUtils.Object, mockCalc.Object);

        var res = await handler.Handle(new GetEmployeeWeeklySummaryQry(empId), CancellationToken.None);

        res.ShouldBeOfType<GenResult<EmployeeWeeklySummaryDto>>();
        res.Succeeded.ShouldBeTrue();
        res.Value!.EmployeeId.ShouldBe(empId);
        res.Value!.EmployeeName.ShouldBe(emp.Name);
        res.Value!.HoursWorked.ShouldBe(2.0);
        res.Value!.TipShare.ShouldBe(25.0);

        mockUtils.Verify(u => u.GetMostRecentMonday(It.IsAny<DateTime>()), Times.Once);
        mockShiftRepo.Verify(r => r.GetTotalSecondsForEmployeeInRangeAsync(empId, It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
        mockShiftRepo.Verify(r => r.GetTotalSecondsAllInRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
        mockTipRepo.Verify(r => r.GetTotalTipsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
