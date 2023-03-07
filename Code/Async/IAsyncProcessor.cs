using System;

namespace UnityFoundation.Code
{
    public interface IAsyncProcessor
    {
        void ExecuteEveryFrame(Action callback);
        void ExecuteEveryFrame(Action<float> callbackWithTime);
        void ProcessAsync(Action action, float delay);
        void ResetCallbackEveryFrame();
    }
}
