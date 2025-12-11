using LogToFile.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace LogToFile.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly IDisposable? _onChangeToken;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private LogToFileConfig _currentConfig;
    private readonly ConcurrentDictionary<string, FileLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    //-------------------------------//

    public FileLoggerProvider(IHttpContextAccessor httpContextAccessor, IOptionsMonitor<LogToFileConfig> config)
    {
        _currentConfig = config.CurrentValue;
        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        _httpContextAccessor = httpContextAccessor;
    }//Ctor

    //--------------------------// 

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new FileLogger(name, GetCurrentConfig, _httpContextAccessor));

    //-------------------------------//

    private LogToFileConfig GetCurrentConfig() => _currentConfig;

    //-------------------------------//

    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken?.Dispose();
    }

    //-------------------------------//

}//Cls
