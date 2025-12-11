using JustTip.Application.Domain.Entities.OutboxMessages;

namespace JustTip.Application.Utility;
public class JtMsgs
{
    static readonly string _nl = Environment.NewLine;

    //#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#//

    internal static class Info
    {

        public static readonly string ORDER_COMPLETE = $"Your complete has been place in our system.{_nl}You will receive a Confirmation Email Shortly.{_nl} Check your spam folder if you don't see it.";
        public static readonly string ALREADY_ENTERED = $"Your complete has already been entered.";
        public static string Deleted<T>(Guid id) => $"{typeof(T).Name}, '{id}' was deleted.";


    }

    //#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#//

    public static class Error
    {
        public const string NO_DATA_SUPPLIED = "No data supplied";
        public static string IsRequired(string propertyName) => $"{propertyName} is required.";
        public static string NotFound<T>(object? id) => $"{typeof(T).Name}, '{id ?? "_blank_"}' was not found.";


        public static class Jobs
        {

            public static string MISSING_OUTBOX_CONTENT(JtOutboxMessage msg) => $"Date:{DateTime.UtcNow} Missing outbox content for: {msg}";


        }//Cls JOBS
    }

    //#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#=#//
}//Cls
