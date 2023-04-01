using System;

namespace UnityFoundation.Code.DebugHelper
{
    public interface IBilucaLogger
    {
        void Log(params string[] message);
        void LogHighlight(string highlight, params string[] message);
        void LogWarning(params string[] message);
        void Error(Exception exception);
        void Setup(LogLevels logLevel);

        public enum LogLevels
        {
            None,
            Error,
            Warn,
            Info
        }
    }
}
