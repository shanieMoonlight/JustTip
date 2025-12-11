using Microsoft.Extensions.Logging;

namespace LogToFile.Setup;

public class LogToFileSetupOptions
{
    /// <summary>
    /// Name of file to write to. Default =  <inheritdoc cref="DefaultValues.FileName"/>
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Type of file to write to. Default =  <inheritdoc cref="DefaultValues.FileExtension"/>
    /// </summary>
    public string FileExtension { get; set; } = DefaultValues.FileExtension;

    /// <summary>
    /// Type of file to write to. Default =  <inheritdoc cref="DefaultValues.FileDirectory"/>
    /// </summary>
    public string FileDirectory { get; set; } = DefaultValues.FileDirectory;

    /// <summary>
    /// Title of App. For identification.
    /// </summary>
    public string AppName { get; set; } = string.Empty;

    /// <summary>
    /// Company using Application
    /// </summary>
    public string Company { get; set; } = string.Empty;


    /// <summary>
    /// Maximum length of Log Message
    /// </summary>
    public int MaxMessageLength { get; set; } = DefaultValues.MAX_MESSAGE_LENGTH;

    /// <summary>
    /// Minimum level that the event must be in order to log it. <br/>
    /// Default = LogLevel.Error
    /// </summary>
    public LogLevel MinLevel { get; set; } = LogLevel.Error;


    /// <summary>
    /// Minimum level that the event must be in order to log it. <br/>
    /// Default = LogLevel.Error
    /// </summary>
    public Func<string, LogLevel, bool>? Filter
    {
        get => filter ?? ((_, logLevel) => logLevel >= MinLevel);
        set => filter = value;
    }
    private Func<string, LogLevel, bool>? filter = null;

}//Cls

