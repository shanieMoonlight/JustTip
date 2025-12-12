using JustTip.Application.LocalServices.Abs;

namespace JustTip.Application.LocalServices.Imps;

public class TipCalculatorService : ITipCalculatorService
{
    public TipSplit  Calculate(long employeeSeconds, long totalSeconds, decimal totalTips)
    {
        double hours = employeeSeconds / 3600.0;
        decimal tipShare = 0m;
        if (totalSeconds > 0)
        {
            tipShare = Math.Round(totalTips * (employeeSeconds / (decimal)totalSeconds), 2);
        }

        return new TipSplit(hours, tipShare);
    }
}
