using System;

namespace UnityFoundation.Code
{
    public interface IAsyncProcessor
    {
        void ProcessAsync(Action action, float startTime);
    }
}
