using Microsoft.Extensions.Logging;
using System;

namespace FL.Logging.Implementation.Standard
{
    public class Logger<T>: LogTrace.Contracts.Standard.ILogger<T>
    {
        ILogger<T> iLogger;

        public Logger(ILogger<T> iLogger)
        {
            this.iLogger = iLogger;
        }

        public void LogDebug(string message)
        {
            this.iLogger.LogDebug(message);
        }

        public void LogDebug(Exception exception, string message = "")
        {
            this.iLogger.LogDebug(exception, message);
        }

        public void LogInfo(string message)
        {
            this.iLogger.LogInformation(message);
        }

        public void LogInfo(Exception exception, string message = "")
        {
            this.iLogger.LogInformation(exception, string.Empty);
        }

        public void LogWarning(string message)
        {
            this.iLogger.LogWarning(message);
        }

        public void LogWarning(Exception exception, string message = "")
        {
            this.iLogger.LogWarning(exception, string.Empty);
        }

        public void LogError(string message)
        {
            this.iLogger.LogError(message);
        }

        public void LogError(Exception exception, string message = "")
        {
            this.iLogger.LogError(exception, string.Empty);
        }
    }
}