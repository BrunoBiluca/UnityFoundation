using System;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Code.Timer
{
    public class Timer : ITimer
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
        public float CurrentTime {
            get {
                if(timerBehaviour == null)
                    return 0f;

                return timerBehaviour.Timer;
            }
        }

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
        public bool Completed => MathX.NearlyEqual(CurrentTime, amount, 0.01f);

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
        private float amount;
        private bool isLoop;
        private readonly Action callback;

        /// <summary>
        /// Instantiate a gameobject to run the timer for some provider action, by default run once and stop
        /// </summary>
        /// <param name="amount">time in seconds</param>
        public Timer(float amount)
        {
            this.amount = amount;
            callback = () => { };

            isLoop = false;
        }

        /// <summary>
        /// Instantiate a gameobject to run the timer for some provider action, by default run once and stop
        /// </summary>
        /// <param name="amount">time in seconds</param>
        /// <param name="callback">callback called when amount of time is reached</param>
        public Timer(float amount, Action callback)
        {
            this.amount = amount;
            this.callback = callback;

            isLoop = true;
        }

        public ITimer SetAmount(float newAmount)
        {
            amount = newAmount;
            return this;
        }

        public ITimer SetName(string name)
        {
            this.name = name;
            return this;
        }

        public ITimer RunOnce()
        {
            isLoop = false;
            return this;
        }

        public ITimer Loop()
        {
            isLoop = true;
            return this;
        }

        /// <summary>
        /// Start the timer with the setting parameters
        /// </summary>
        public ITimer Start()
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
            if(timerBehaviour == null)
                return;
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