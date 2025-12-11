using Hangfire.Common;
using Hangfire.Storage;
using JustTip.Application.Jobs.Dtos;

namespace JustTip.Infrastructure.Jobs.Utils;
internal static class JobDtoExtensions
{
    internal static JtRecurringJobDto ToRecurringJobDto(this RecurringJobDto hfDto)
    {
        return new(
            hfDto.Id,
            hfDto.Cron,
            hfDto.Queue,
            hfDto.Job.ToJobDto(),
            hfDto.LoadException,
            hfDto.NextExecution,
            hfDto.LastJobId,
            hfDto.LastJobState,
            hfDto.LastExecution,
            hfDto.CreatedAt,
            hfDto.Removed,
            hfDto.TimeZoneId,
            hfDto.Error,
            hfDto.RetryAttempt);
    }

    //--------------------------// 

    internal static JtJobDto ToJobDto(this Job hfDto) =>
        new(hfDto.Type.Name, hfDto.Method.Name, hfDto.Args);

}//Cls
