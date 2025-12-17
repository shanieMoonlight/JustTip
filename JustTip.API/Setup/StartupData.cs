namespace JustTip.API.Setup;
public class JtStartupData(IConfiguration config)
    : StronglyTypedAppSettings.AppSettingsAccessor(config)
{

    /// <summary>
    /// Default: "dist"
    /// </summary>
    public static string SPA_DIST_FOLDER { get; set; } = "dist";

    /// <summary>
    /// Default: "ClientApp"
    /// </summary>
    public static string SPA_ROOT { get; set; } = "ClientApp";


    /// <summary>
    /// Full Path to the production distribution directory.
    /// </summary>
    protected string DIST_DIRECTORY_PROD = Path.Combine(Directory.GetCurrentDirectory(), SPA_ROOT, SPA_DIST_FOLDER);


    /// <summary>
    /// Path to the static files for the Single Page Application (SPA).
    /// </summary>
    public string SPA_STATIC_FILES_PATH = Path.Combine(SPA_ROOT, SPA_DIST_FOLDER);

}
