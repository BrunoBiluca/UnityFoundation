using System;

namespace UnityFoundation.Code
{
    public class UpdatableCallback : IUpdatable
    {
        private readonly Action<float> callback;

        public UpdatableCallback(Action<float> callback)
        {
            this.callback = callback;
        }

        public void Update(float deltaTime = 0)
        {
            callback(deltaTime);
        }
    }
}