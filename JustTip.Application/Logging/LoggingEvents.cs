namespace JustTip.Application.Logging;

public class JtLoggingEvents
{


    public static class JOBS
    {
        public static readonly int OUTBOX_PROCESSING = 100;
        public static readonly int OLD_OUTBOX_MSGS_PROCESSING = OUTBOX_PROCESSING + 1;
        public static readonly int OLD_LOGS_PROCESSING = OLD_OUTBOX_MSGS_PROCESSING + 1;
    }



    public static class EventHandling
    {
        public static readonly int EMPLOYEES = 1000;
        public static readonly int SHIFTS = EMPLOYEES + 1;
    }




}//Cls
