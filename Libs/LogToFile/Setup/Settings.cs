using Microsoft.Extensions.Logging;

namespace LogToFile.Setup;

internal class Settings
{
    /// <summary>
    /// Name of file to write to. Default =  <inheritdoc cref="DefaultValues.FileName"/>
    /// </summary>
    internal static string FileName { get; set; } = DefaultValues.FileName;

    /// <summary>
    /// Type of file to write to. Default =  <inheritdoc cref="DefaultValues.FileExtension"/>
    /// </summary>
    internal static string FileExtension { get; set; } = DefaultValues.FileExtension;

    /// <summary>
    /// Type of file to write to. Default =  <inheritdoc cref="DefaultValues.FileDirectory"/>
    /// </summary>
    internal static string FileDirectory { get; set; } = DefaultValues.FileDirectory;

    /// <summary>
    ///Full path to Log File
    /// </summary>
    internal static string FileFullPath
    {
        get
        {
            var fullName = Path.ChangeExtension(FileName, FileExtension);
            return Path.Combine(FileDirectory, fullName);
        }
    }

    /// <summary>
    /// Filter to decide whether to log an event or not
    /// </summary>
    internal static Func<string, LogLevel, bool>? Filter;

    /// <summary>
    /// Maximum length of Log Message. Default =  <inheritdoc cref="DefaultValues.MAX_MESSAGE_LENGTH"/>
    /// </summary>
    internal static int MaxMessageLength { get; set; } = DefaultValues.MAX_MESSAGE_LENGTH;

    /// <summary>
    /// Title of App. For identification.
    /// </summary>
    internal static string AppName { get; set; } = string.Empty;

    /// <summary>
    /// Company using Application
    /// </summary>
    internal static string Company { get; set; } = string.Empty;



    //--------------------------//

    internal static void Setup(LogToFileSetupOptions options)
    {

        if (options == null)
            throw new ArgumentNullException(nameof(options), "You must at least supply the AppName.");

        if (string.IsNullOrWhiteSpace(options.AppName))
            throw new ArgumentNullException(nameof(options), "You must supply an Application name when using LogToFile");
        else
            AppName = options.AppName;

        FileDirectory = options.FileDirectory;
        FileExtension = options.FileExtension;

        Company = options.Company ?? AppName;

        if (string.IsNullOrWhiteSpace(options.FileName))
            FileName = $"{Company}_{DefaultValues.FileName}";
        else
            FileName = options.FileName;

        MaxMessageLength = options.MaxMessageLength;

    }

}//Cls

