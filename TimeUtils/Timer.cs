using System;
using UnityEngine;

namespace Assets.UnityFoundation.TimeUtils {
    public class Timer {
        private class TimerMonoBehaviour : MonoBehaviour {
            [SerializeField]
            private float timerMax;

            [SerializeField]
            private float timer;
            public float Timer {
                get { return timer; }
                set { timer = value; }
            }
            private Action callback;

            public void Setup(float amount, Action callback) {
                timer = 0f;
                timerMax = amount;
                this.callback = callback;
            }

            private void Update() {
                timer += Time.deltaTime;

                if(timer >= timerMax) {
                    timer = 0f;
                    try {
                        callback();
                    } catch(MissingReferenceException) {
                        Close();
                    }
                }
            }

            internal void Close() {
                if(gameObject == null) return;
                Destroy(gameObject);
            }
        }

        private readonly TimerMonoBehaviour timerBehaviour;
        private readonly float amount;
        private readonly Action callback;

        public Timer(string name, float amount, Action callback) {
            this.amount = amount;
            this.callback = callback;

            var timersRef = GameObject.Find("** Timers");
            if(timersRef == null) {
                timersRef = new GameObject("** Timers");
            }

            timerBehaviour = new GameObject(name).AddComponent<TimerMonoBehaviour>();
            timerBehaviour.transform.SetParent(timersRef.transform);
            timerBehaviour.Setup(amount, callback);
        }

        public void Restart() {
            timerBehaviour.Setup(amount, callback);
        }

        public void Close() {
            if(timerBehaviour == null) return;
            timerBehaviour.Close();
        }

        public float GetCurrentTime() {
            return amount - timerBehaviour.Timer;
        }

    }
}