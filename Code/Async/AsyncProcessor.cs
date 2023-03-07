using System;
using System.Collections;
using UnityEngine;

namespace UnityFoundation.Code
{

    public class AsyncProcessor : Singleton<AsyncProcessor>, IAsyncProcessor
    {
        public override bool DestroyOnLoad { get; set; } = false;

        // TOOD: funciona apenas para um callback por vez,
        // essa lógica deve permitir executar para múltiplos callbacks
        private Action<float> callbackEveryFrameWithTime;
        private Action callbackEveryFrame;

        private bool isDelayCallbackExecuted = true;
        private float delay;
        private Action delayCallback;

        public void ExecuteEveryFrame(Action<float> callback)
        {
            callbackEveryFrameWithTime = callback;
        }

        public void ProcessAsync(Action action, float delay)
        {
            StartCoroutine(Callback(action, delay));
        }

        public void ResetCallbackEveryFrame()
        {
            callbackEveryFrameWithTime = null;
        }

        public void ExecuteEveryFrame(Action callback)
        {
            callbackEveryFrame = callback;
        }

        private IEnumerator Callback(Action action, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            action();
        }

        public void Update()
        {
            callbackEveryFrame?.Invoke();
            callbackEveryFrameWithTime?.Invoke(Time.deltaTime);

            if(!isDelayCallbackExecuted)
            {
                delay -= Time.deltaTime;
                if(delay < 0) {
                    isDelayCallbackExecuted = true;
                    delayCallback();
                }
            }
        }

        public void ExecuteWithDelay(float delay, Action callback)
        {
            isDelayCallbackExecuted = false;
            this.delay = delay;
            delayCallback = callback;
        }
    }
}
