using LogToFile.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LogToFile.Logging;

public class FileLogger : ILogger
{
    private readonly string _categoryName;
    private readonly IHttpContextAccessor? _httpContextAccessor;
    private readonly Func<LogToFileConfig> _getCurrentConfig;
    private static readonly string _nl = Environment.NewLine;
    private static readonly string _divider = $"{_nl}{_nl}################################################################################################################################################################{_nl}#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#{_nl}################################################################################################################################################################{_nl}{_nl}";

    //-------------------------------//

    public FileLogger(string categoryName, Func<LogToFileConfig> getCurrentConfig, IHttpContextAccessor httpContextAccessor) =>
        (_categoryName, _getCurrentConfig, _httpContextAccessor) = (categoryName, getCurrentConfig, httpContextAccessor);

    //-------------------------------//

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        //if (!ShouldLogEvent(eventId))
        //    return;

        ArgumentNullException.ThrowIfNull(formatter, nameof(formatter));


        var report = InfoMessageBuilder.BuildLoggingInfoMessage(
           eventId, logLevel, state, exception, formatter, _httpContextAccessor?.HttpContext, Settings.AppName, Settings.MaxMessageLength);

            WriteToFile(report);

    }

    //-------------------------------//

    private bool ShouldLogEvent(EventId eventId)
    {
        var config = _getCurrentConfig();

        //If specific eventIds have not been set, then they should all be logged
        if (config.EventIds == null || config.EventIds.Count == 0)
            return true;

        if (config.EventIds.Contains(eventId.Id))
            return true;


        return true;
    }

    //-------------------------------//

    private static void WriteToFile(string report)
    {
        try
        {
            PrependToFile(Settings.FileFullPath, $"{_nl}{_divider}{_nl}");
            PrependToFile(Settings.FileFullPath, report);
        }
        catch (Exception)
        {
            //Skip it
        }
    }

    //-------------------------------//

    public static void PrependToFile(string filePath, string text)
    {
        try
        {
            // Read the existing content
            string existingContent = "";
            if (File.Exists(filePath))
                existingContent = File.ReadAllText(filePath);

            // Prepend the new text
            string newContent = text + Environment.NewLine + existingContent;

            // Write the new content back to the file
            File.WriteAllText(filePath, newContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error prepending text to {filePath}: {ex.Message}");
            throw; // Re-throw the exception to allow calling code to handle it
        }
    }

    //-------------------------------//

    /// <summary>
    /// Method to decide whether to log an event or not.
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel) =>
        Settings.Filter == null || Settings.Filter(_categoryName, logLevel);//IsEnabled

    //-------------------------------//

    IDisposable ILogger.BeginScope<TState>(TState state) => default!;

    //-------------------------------//

}//Cls


