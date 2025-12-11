namespace JustTip.Application.Logging;
public class JtEventIds
{
    public static class Errors
    {
        public const int General = 1000;
        public const int Unexpected = General + 1;
        public const int NotFound = Unexpected + 1;
        public const int Validation = NotFound + 1;
    }
}
