using Contract.Interfaces;
using NLog;

namespace LoggerService.Services;

public class LoggerManager : ILoggerManager
{
    private static ILogger _logger = LogManager.GetCurrentClassLogger();
    public LoggerManager()
    {
        //TODO
    }
    public void LogDebug(string message)
    {
        throw new NotImplementedException();
    }

    public void LogError(string message)
    {
        throw new NotImplementedException();
    }

    public void LogInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void LogWarn(string message)
    {
        throw new NotImplementedException();
    }
}
