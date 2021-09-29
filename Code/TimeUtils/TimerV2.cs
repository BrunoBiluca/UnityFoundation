using System;
using UnityEngine;

namespace Assets.UnityFoundation.Code.TimeUtils
{
    public class TimerV2
    {
        private static GameObject timersReference;

        private static void TryGetTimersReference()
        {
            if(timersReference != null) return;

            var timersRef = GameObject.Find("** Timers");
            if(timersRef == null)
            {
                timersRef = new GameObject("** Timers");
            }

            timersReference = timersRef;
        }

        /// <summary>
        /// Get the time passed in the current loop in seconds
        /// </summary>
        public float CurrentTime { get { return timerBehaviour.Timer; } }

        /// <summary>
        /// Get the time to end the current loop in seconds
        /// </summary>
        public float RemainingTime { get { return amount - timerBehaviour.Timer; } }

        /// <summary>
        /// Get the completion percentage of the timer
        /// </summary>
        public float Completion {
            get {
                return CurrentTime / amount * 100f;
            }
        }

        /// <summary>
        /// Get if the timer finished it's execution
        /// </summary>
        public bool Completed => MathX.NearlyEqual(CurrentTime, amount, 0.1f);

        /// <summary>
        /// Get if the timer is current running
        /// </summary>
        public bool IsRunning {
            get {
                return timerBehaviour != null && timerBehaviour.IsRunning;
            }
        }

        private TimerMonoBehaviour timerBehaviour;
        private string name;
        private readonly float amount;
        private bool isLoop;
        private readonly Action callback;
        
        /// <summary>
        /// Instantiate a gameobject to run the timer for some provider action, by default run once and stop
        /// </summary>
        /// <param name="amount">time in seconds</param>
        public TimerV2(float amount)
        {
            this.amount = amount;
            this.callback = () => { };

            isLoop = false;
        }

        /// <summary>
        /// Instantiate a gameobject to run the timer for some provider action, by default run once and stop
        /// </summary>
        /// <param name="amount">time in seconds</param>
        /// <param name="callback">callback called when amount of time is reached</param>
        public TimerV2(float amount, Action callback)
        {
            this.amount = amount;
            this.callback = callback;

            isLoop = true;
        }

        public TimerV2 SetName(string name)
        {
            this.name = name;
            return this;
        }

        public TimerV2 RunOnce()
        {
            isLoop = false;
            return this;
        }

        public TimerV2 Loop()
        {
            isLoop = true;
            return this;
        }

        /// <summary>
        /// Start the timer with the setting parameters
        /// </summary>
        public TimerV2 Start()
        {
            if(timerBehaviour == null)
                InstantiateTimer();

            timerBehaviour.Setup(amount, callback, isLoop);

            return this;
        }

        /// <summary>
        /// Stop running the timer
        /// </summary>
        public void Stop()
        {
            timerBehaviour.Deactivate();
        }

        public void Resume()
        {
            timerBehaviour.Activate();
        }

        public void Close()
        {
            if(timerBehaviour == null) return;
            timerBehaviour.Close();
        }

        private void InstantiateTimer()
        {
            TryGetTimersReference();

            if(string.IsNullOrEmpty(name)) name = Guid.NewGuid().ToString();

            timerBehaviour = new GameObject(name).AddComponent<TimerMonoBehaviour>();
            timerBehaviour.transform.SetParent(timersReference.transform);
        }
    }
}