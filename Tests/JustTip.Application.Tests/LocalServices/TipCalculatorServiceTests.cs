using JustTip.Application.LocalServices.Imps;

namespace JustTip.Application.Tests.LocalServices;

public class TipCalculatorServiceTests
{
    [Fact]
    public void Calculate_ReturnsHoursAndTipShare_WhenTotalSecondsPositive()
    {
        var svc = new TipCalculatorService();

        long employeeSeconds = 2 * 3600; // 2 hours
        long totalSeconds = 8 * 3600; // 8 hours total
        decimal totalTips = 100m;

        var result = svc.Calculate(employeeSeconds, totalSeconds, totalTips);

        result.Hours.ShouldBe(2.0);
        result.TipShare.ShouldBe(25.00m);
    }

    //----------------------//

    [Fact]
    public void Calculate_ReturnsZeroTipShare_WhenTotalSecondsZero()
    {
        var svc = new TipCalculatorService();

        long employeeSeconds = 3600;
        long totalSeconds = 0;
        decimal totalTips = 50m;

        var result = svc.Calculate(employeeSeconds, totalSeconds, totalTips);

        result.Hours.ShouldBe(1.0);
        result.TipShare.ShouldBe(0m);
    }

    //----------------------//

    [Fact]
    public void Calculate_ReturnsZeroHoursAndZeroTip_WhenEmployeeSecondsZero()
    {
        var svc = new TipCalculatorService();

        long employeeSeconds = 0;
        long totalSeconds = 10 * 3600;
        decimal totalTips = 200m;

        var result = svc.Calculate(employeeSeconds, totalSeconds, totalTips);

        result.Hours.ShouldBe(0.0);
        result.TipShare.ShouldBe(0m);
    }

    //----------------------//

    [Fact]
    public void Calculate_RoundsTipShareToTwoDecimals()
    {
        var svc = new TipCalculatorService();

        long employeeSeconds = 1; // tiny fraction
        long totalSeconds = 3;
        decimal totalTips = 1m; // 1.00 -> share = 0.333333..., round to 0.33

        var result = svc.Calculate(employeeSeconds, totalSeconds, totalTips);

        result.Hours.ShouldBe(1.0/3600.0);
        result.TipShare.ShouldBe(0.33m);
    }
}
