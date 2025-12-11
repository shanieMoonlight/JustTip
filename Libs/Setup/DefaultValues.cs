namespace LogToFile.Setup;

internal class DefaultValues
{
    /// <summary>
    /// 100000
    /// </summary>
    public const int MAX_MESSAGE_LENGTH = 100000;

    /// <summary>
    /// Plain Text (.txt)
    /// </summary>
    public const string FileExtension = ".txt";


    /// <summary>
    /// "Logging Report"
    /// </summary>
    public const string FileName = "Logging_Report.txt";

    /// <summary>
    /// Environment.SpecialFolder.Desktop
    /// </summary>
    public static readonly string FileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

}//Cls

