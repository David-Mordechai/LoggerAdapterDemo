using JetBrains.Annotations;
namespace WorkerService1.Logger;

public interface ILoggerAdapter<T>
{
    void LogInformation(LogState logState, LogAction logAction, [StructuredMessageTemplate]string message, params object[] args);
}

public class LoggerAdapter<T>(ILogger<T> logger) : ILoggerAdapter<T>
{
    public void LogInformation(LogState logState, LogAction logAction, string? message, params object[] args)
    {
        var extendMessage = $"State: [{{logState}}] Action: [{{logAction}}] - {message}";
        object[] additionalArgs = [logState, logAction];
        var extendArgs = additionalArgs.Concat(args).ToArray();
        logger.LogInformation(extendMessage, extendArgs);
    }
}