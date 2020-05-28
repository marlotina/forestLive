using System;

namespace FL.LogTrace.Contracts.Standard
{
    public interface ILogger<out T>
    {
        void LogDebug(string message);
        void LogDebug(Exception exception, string message = "");

        void LogInfo(string message);
        void LogInfo(Exception exception, string message = "");

        void LogWarning(string message);
        void LogWarning(Exception exception, string message = "");

        void LogError(string message);
        void LogError(Exception exception, string message = "");
    }
}
