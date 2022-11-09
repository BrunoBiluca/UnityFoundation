using System;

namespace UnityFoundation.Code
{
    public interface IAsyncProcessor
    {
        void ExecuteEveryFrame(Action<float> rotateUnit);
        void ProcessAsync(Action action, float delay);
        void ResetCallbackEveryFrame();
    }
}
