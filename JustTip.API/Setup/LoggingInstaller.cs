namespace JustTip.API.Setup;

using LogToFile.Setup;

/// <summary>
/// Install Logging stuff
/// </summary>
public class LoggingInstaller
{

    /// <summary>
    /// Install some Logging dependencies
    /// </summary>
    /// <param name="builder">Application Builder</param>
    /// <param name="startupData">All the app config and settings</param>
    public WebApplicationBuilder Install(WebApplicationBuilder builder, JtStartupData startupData)
    {

        var logging = builder.Logging; ;
        var env = builder.Environment;

        logging.ClearProviders();

        logging.AddLogToFile(config =>
        {
            //config.AppName = startupData.GetAppName();
            config.Company = "JustTip";
        });

        return builder;

    }

}//Cls


//######################################################################//


/// <summary>
/// Install Logging stuff
/// </summary>
public static class LoggingInstallerExtensions
{

    /// <summary>
    /// Install some Logging dependencies
    /// </summary>
    /// <param name="builder">Application Builder</param>
    /// <param name="startupData">All the app config and settings</param>
    public static WebApplicationBuilder InstallLogging(this WebApplicationBuilder builder, JtStartupData startupData) =>
        new LoggingInstaller().Install(builder, startupData);

}//Cls

