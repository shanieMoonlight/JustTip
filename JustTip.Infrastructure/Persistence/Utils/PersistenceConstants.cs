namespace JustTip.Infrastructure.Persistence.Utils;
internal class JtPersistenceConfigConstants
{
    internal static class Config
    {
        public const int NAME_MAX_LENGTH = 128;
        public const int DESCRIPTION_MAX_LENGTH = 1024;

    }
    internal static class Db
    {
        public const string SCHEMA = "Jt";
        public const string MIGRATIONS_HISTORY_TABLE = "__EFMigrationsHistory";
    }


}
