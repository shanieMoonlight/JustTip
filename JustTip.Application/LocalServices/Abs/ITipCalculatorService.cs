namespace JustTip.Application.LocalServices.Abs;

public record TipSplit(double Hours, decimal TipShare);

public interface ITipCalculatorService
{
    /// <summary>
    /// Calculate hours and tip share for an employee given seconds worked, total seconds and total tips.
    /// Returns hours (double) and tipShare (decimal, rounded to 2 decimals).
    /// </summary>
    TipSplit Calculate(long employeeSeconds, long totalSeconds, decimal totalTips);
}
