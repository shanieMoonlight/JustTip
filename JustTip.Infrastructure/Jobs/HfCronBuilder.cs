using Hangfire;
using JustTip.Application.Jobs.Abstractions;

namespace JustTip.Infrastructure.Jobs;

/// <summary>
/// Produces HF-compatible cron expressions.
/// </summary>
internal class HfCronBuilder : ICronBuilder
{

    // Every minute
    public string Minutely() => Cron.Minutely();

    // Every hour at minute 0
    public string Hourly() => Cron.Hourly();

    // Every hour at specified minute
    public string Hourly(int minute) => Cron.Hourly(minute);

    public string Daily() => Cron.Daily();

    public string Weekly() => Cron.Weekly(); // Monday

    public string Weekly(DayOfWeek dayOfWeek) => Cron.Weekly(dayOfWeek);

    public string Weekly(DayOfWeek dayOfWeek, int hour) => Cron.Weekly(dayOfWeek, hour);

    public string Monthly() => Cron.Monthly();  

    public string Monthly(int day) => Cron.Monthly(day);

    public string Monthly(int day, int hour) => Cron.Monthly(day, hour);
    public string Yearly() => Cron.Yearly();

    public string Yearly(int month)  => Cron.Yearly(month);

    public string Yearly(int month, int day) => Cron.Yearly(month, day);

    public string Yearly(int month, int day, int hour) => Cron.Yearly(month, day, hour);


    public string Never() =>Cron.Never(); 

    public string MinuteInterval(int minutes) => Cron.MinuteInterval(minutes);


}//Cls
