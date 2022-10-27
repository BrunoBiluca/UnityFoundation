using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code.DebugHelper.Tests
{
    public class DefaultLoggerTests : MonoBehaviour
    {
        [Test]
        public void Should_log_info_when_configure_log_level_info()
        {
            var logHandler = new Mock<IBilucaLogHandler>();

            var logger = new DefaultLogger(IBilucaLogger.LogLevels.Info, logHandler.Object);

            foreach(var level in GetAllLogLevels())
            {
                logger.Setup(level);
                logger.Log("test", "message");
            }

            logHandler.Verify(lh => lh.Log(It.IsAny<string>()), Times.Once());
        }

        private static IEnumerable<IBilucaLogger.LogLevels> GetAllLogLevels()
        {
            return Enum.GetValues(typeof(IBilucaLogger.LogLevels))
                .Cast<IBilucaLogger.LogLevels>();
        }

        [Test]
        public void Should_log_warn_when_configure_log_level_info_or_warn()
        {
            var logHandler = new Mock<IBilucaLogHandler>();

            var logger = new DefaultLogger(IBilucaLogger.LogLevels.None, logHandler.Object);

            foreach(var level in GetAllLogLevels())
            {
                logger.Setup(level);
                logger.LogWarning("test", "message");
            }

            logHandler.Verify(lh => lh.Warn(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
