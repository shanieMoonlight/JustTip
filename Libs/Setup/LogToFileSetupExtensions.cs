using LogToFile.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace LogToFile.Setup;

public static class LogToFileSetupExtensions
{

    private static ILoggingBuilder UseLogToFile(this ILoggingBuilder builder, Func<string, LogLevel, bool>? filter = null)
    {
        Settings.Filter = filter;

        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions<LogToFileConfig, FileLoggerProvider>(builder.Services);

        return builder;

    }

    //- - - - - - - - - - - //

    private static ILoggingBuilder UseLogToFile(this ILoggingBuilder factory, LogLevel minLevel) =>
        factory.UseLogToFile((_, logLevel) => logLevel >= minLevel);

    //----------------------//

    /// <summary>
    /// Set up Logger for use in app. 
    /// </summary>
    /// <param name="builder">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="setupOptions">Specific setup details</param>
    /// <returns>IServiceCollection with LogToDatase attached</returns>
    public static ILoggingBuilder AddLogToFile(this ILoggingBuilder builder, LogToFileSetupOptions setupOptions)
    {

        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        Settings.Setup(setupOptions);

        if (setupOptions.Filter != null)
            builder.UseLogToFile(setupOptions.Filter);
        else
            builder.UseLogToFile(setupOptions.MinLevel);

        return builder;

    }

    //----------------------//

    /// <summary>
    /// Set up Logger for use in app. 
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="optionsConfig">Action describing how to set up LogToFileSetupOptions</param>
    /// <returns>IServiceCollection with LogToFile attached</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ILoggingBuilder AddLogToFile(this ILoggingBuilder builder, Action<LogToFileSetupOptions> optionsConfig)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var setupOptions = new LogToFileSetupOptions();
        optionsConfig(setupOptions);

        builder.AddLogToFile(setupOptions);

        return builder;

    }

    //----------------------//

    /// <summary>
    /// Set up Logger for use in app. 
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="optionsConfig">Action describing how to set up LogToFileSetupOptions</param>
    /// <param name="loggerConfig">Action describing Logging will be done - DONT USE</param>
    /// <returns>IServiceCollection with LogToFile attached</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ILoggingBuilder AddLogToFile(
        this ILoggingBuilder builder, Action<LogToFileSetupOptions> optionsConfig, Action<LogToFileConfig> loggerConfig)
    {

        builder.AddLogToFile(optionsConfig);

        builder.Services.Configure(loggerConfig);

        return builder;

    }

}//Cls


