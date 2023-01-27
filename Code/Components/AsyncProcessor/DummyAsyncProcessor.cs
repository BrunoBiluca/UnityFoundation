using System;

namespace UnityFoundation.Code
{
    public class DummyAsyncProcessor : IAsyncProcessor
    {
        float timePassed;
        float startTime;
        Action action;

        public void ExecuteEveryFrame(Action<float> rotateUnit)
        {
            throw new NotImplementedException();
        }

        public void ExecuteEveryFrame(Action callback)
        {
            throw new NotImplementedException();
        }

        public void ProcessAsync(Action action, float startTime)
        {
            timePassed = 0f;
            this.startTime = startTime;
            this.action = action;
        }

        public void ResetCallbackEveryFrame()
        {
            throw new NotImplementedException();
        }

        public void Update(float timePassed)
        {
            this.timePassed += timePassed;
            if(this.timePassed >= this.startTime)
                action();
        }
    }
}
