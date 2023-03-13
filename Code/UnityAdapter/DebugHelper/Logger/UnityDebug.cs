using System;
using UnityEngine;

namespace UnityFoundation.Code.DebugHelper
{
    public class UnityDebug : Singleton<UnityDebug>, IBilucaLogger
    {
        [field: SerializeField]
        public IBilucaLogger.LogLevels LogLevel { get; private set; }

        private IBilucaLogger logger;

        protected override void PreAwake()
        {
            LogLevel = IBilucaLogger.LogLevels.Info;
            logger = new DefaultLogger(LogLevel, new UnityLogHandler());
        }

        public void Setup(IBilucaLogger.LogLevels logLevel)
        {
            LogLevel = logLevel;
            logger.Setup(LogLevel);
        }

        public void LogHighlight(string highlight, params string[] message)
        {
            logger.Setup(LogLevel);
            logger.LogHighlight(highlight, message);
        }

        public void Log(params string[] message)
        {
            logger.Setup(LogLevel);
            logger.Log(message);
        }

        public void LogWarning(params string[] message)
        {
            logger.Setup(LogLevel);
            logger.LogWarning(message);
        }

        public void Error(Exception exception)
        {
            logger.Setup(LogLevel);
            logger.Error(exception);
        }
    }
}
