using System;
using UnityEngine;

namespace Assets.UnityFoundation.TimeUtils {
    public class Timer {
        private class TimerMonoBehaviour : MonoBehaviour {
            [SerializeField]
            private bool runOnce;

            [SerializeField]
            private float timerMax;

            [SerializeField]
            private float timer;
            public float Timer {
                get { return timer; }
                set { timer = value; }
            }
            private Action callback;

            public void Setup(float amount, Action callback, bool runOnce) {
                timer = 0f;
                timerMax = amount;
                this.callback = callback;
                this.runOnce = runOnce;
            }

            private void Update() {
                timer += Time.deltaTime;

                if(timer >= timerMax) {
                    timer = 0f;
                    try {
                        callback();
                        if(runOnce) Close();
                    } catch(MissingReferenceException) {
                        Close();
                    }
                }
            }

            public void Close() {
                if(gameObject == null) return;
                Destroy(gameObject);
            }
        }

        private TimerMonoBehaviour timerBehaviour;
        private readonly string name;
        private readonly float amount;
        private readonly bool runOnce;
        private readonly Action callback;

        public Timer(string name, float amount, Action callback, bool runOnce = false) {
            this.name = name;
            this.amount = amount;
            this.callback = callback;
            this.runOnce = runOnce;

            InstantiateTimer();
        }

        public void Restart() {
            if(timerBehaviour != null) {
                timerBehaviour.Setup(amount, callback, runOnce);
                return;
            }

            InstantiateTimer();
        }

        private void InstantiateTimer() {
            // TODO: melhorar esse código para não ficar pesquisando toda
            // a vez que um timer é instanciado,
            // o objeto ** Timers já deve ficar em cache depois da primeira vez que foi criado
            var timersRef = GameObject.Find("** Timers");
            if(timersRef == null) {
                timersRef = new GameObject("** Timers");
            }

            timerBehaviour = new GameObject(name).AddComponent<TimerMonoBehaviour>();
            timerBehaviour.transform.SetParent(timersRef.transform);
            timerBehaviour.Setup(amount, callback, runOnce);
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