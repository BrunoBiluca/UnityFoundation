using System;
using System.Collections;
using UnityEngine;

namespace UnityFoundation.Code
{

    public class AsyncProcessor : Singleton<AsyncProcessor>, IAsyncProcessor
    {
        // TOOD: funciona apenas para um callback por vez,
        // essa l�gica deve permitir executar para m�ltiplos callbacks
        private Action<float> callbackEveryFrameWithTime;
        private Action callbackEveryFrame;

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
        }
    }
}
