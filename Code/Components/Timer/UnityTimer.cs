using UnityEngine;

namespace UnityFoundation.Code.Timer
{
    public class UnityTimer : ITimer
    {
        private float startTime;
        private float currentTime;
        private float amount;

        public bool Completed {
            get {
                Update();
                return startTime + amount < currentTime;
            }
        }

        public float Completion => throw new System.NotImplementedException();

        public float CurrentTime => throw new System.NotImplementedException();

        public bool IsRunning => throw new System.NotImplementedException();

        public float RemainingTime => throw new System.NotImplementedException();

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public ITimer Loop()
        {
            throw new System.NotImplementedException();
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public ITimer RunOnce()
        {
            throw new System.NotImplementedException();
        }

        public ITimer SetAmount(float newAmount)
        {
            amount = newAmount;
            return this;
        }

        public ITimer SetName(string name)
        {
            throw new System.NotImplementedException();
        }

        public ITimer Start()
        {
            startTime = Time.time;
            return this;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            currentTime = Time.time;
        }
    }
}
