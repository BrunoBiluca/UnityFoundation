using System;
using System.Collections;
using UnityEngine;

namespace UnityFoundation.Code
{

    public class AsyncProcessor : Singleton<AsyncProcessor>, IAsyncProcessor
    {
        // TOOD: funciona apenas para um callback por vez,
        // essa l�gica deve permitir executar para m�ltiplos callbacks
        private Action<float> callbackEveryFrame;

        public void ExecuteEveryFrame(Action<float> callback)
        {
            callbackEveryFrame = callback;
        }

        public void ProcessAsync(Action action, float delay)
        {
            StartCoroutine(Callback(action, delay));
        }

        public void ResetCallbackEveryFrame()
        {
            callbackEveryFrame = null;
        }

        private IEnumerator Callback(Action action, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);

            action();
        }

        public void Update()
        {
            callbackEveryFrame?.Invoke(Time.deltaTime);
        }
    }
}
