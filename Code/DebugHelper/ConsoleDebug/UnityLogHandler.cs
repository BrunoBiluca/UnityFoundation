using UnityEngine;

namespace UnityFoundation.Code.DebugHelper
{
    public class UnityLogHandler : IBilucaLogHandler
    {
        public void Error(string message) => Debug.LogError(message);

        public void Log(string message) => Debug.Log(message);

        public void Warn(string message) => Debug.LogWarning(message);
    }
}
