using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code.Timer
{
    public class TimerMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private bool loop;
        [SerializeField] private bool selfDestroyAfterComplete;
        [SerializeField] private float timerMax;

        [SerializeField]
        [ShowOnly]
        private float timer;
        public float Timer {
            get { return timer; }
            set { timer = value; }
        }

        public bool IsRunning => gameObject.activeInHierarchy;

        private Action callback;

        public void Setup(float amount, Action callback, bool loop = true)
        {
            timer = 0f;
            timerMax = amount;
            this.callback = callback;
            this.loop = loop;

            gameObject.SetActive(true);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            if(gameObject == null) return;
            Destroy(gameObject);
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if(timer < timerMax) return;

            try
            {
                FinishCycle();
            }
            catch(MissingReferenceException)
            {
                Close();
            }
        }

        private void FinishCycle()
        {
            callback();

            if(loop)
            {
                timer = 0f;
                return;
            }

            timer = timerMax;
            Deactivate();
        }
    }
}