namespace JustTip.API.Setup;
/// <summary>
/// Install Architecture stuff
/// </summary>
public class ArchitectureInstaller 
{
    /// <summary>
    /// Install some Architecture dependencies
    /// </summary>
    /// <param name="builder">Application Builder</param>
    /// <param name="startupData">All the app config and settings</param>
    public WebApplicationBuilder Install(WebApplicationBuilder builder, JtStartupData startupData)
    {

        //Console.WriteLine($"ArchitectureInstaller: Connection-String: {startupData.ConnectionStringsSection.GetPgDb()}");

        //builder.Services
        //    .AddGbApplication()
        //    .AddGbInfrastructure(startupData.ConnectionStringsSection.GetPgDb())
        //    .AddPresentation();

        return builder;

    }

}//Cls



//######################################################################//

/// <summary>
/// Install Architecture stuff
/// </summary>
public static class ArchitectureInstallerExtensions
{
    /// <summary>
    /// Install some Architecture dependencies
    /// </summary>
    /// <param name="builder">Application Builder</param>
    /// <param name="startupData">All the app config and settings</param>
    public static WebApplicationBuilder InstallArchitecture(this WebApplicationBuilder builder, JtStartupData startupData) =>
        new ArchitectureInstaller().Install(builder, startupData);

}//Cls


//######################################################################//