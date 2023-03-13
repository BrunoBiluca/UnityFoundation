using System;
using System.Collections.Generic;

namespace UnityFoundation.Code.DebugHelper
{
    public class DefaultLogger : IBilucaLogger
    {
        public IBilucaLogger.LogLevels LogLevel { get; private set; }
        public IBilucaLogHandler Handler { get; }

        private bool CanLogInfo => LogLevel >= IBilucaLogger.LogLevels.Info;
        private bool CanLogWarn => LogLevel >= IBilucaLogger.LogLevels.Warn;
        private bool CanLogError => LogLevel >= IBilucaLogger.LogLevels.Error;

        public DefaultLogger(IBilucaLogger.LogLevels logLevel, IBilucaLogHandler handler)
        {
            LogLevel = logLevel;
            Handler = handler;
        }

        public void Setup(IBilucaLogger.LogLevels logLevel)
        {
            LogLevel = logLevel;
        }

        public void LogHighlight(string highlight, params string[] message)
        {
            var newMessage = new List<string> {
                $"[{highlight}] - "
            };
            newMessage.AddRange(message);
            Log(newMessage.ToArray());
        }

        public void Log(params string[] message)
        {
            LogInfo(string.Join(" ", message));
        }

        private void LogInfo(string message)
        {
            if(!CanLogInfo) return;
            Handler.Log(message);
        }

        public void LogWarning(params string[] message)
        {
            if(!CanLogWarn) return;
            Handler.Warn(string.Join(" ", message));
        }

        public void Error(Exception exception)
        {
            if(!CanLogError) return;

            var errorMessage = $"{exception.Message}\n{exception.StackTrace}\n";
            Handler.Error(errorMessage);
        }
    }
}
