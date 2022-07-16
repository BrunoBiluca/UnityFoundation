using System;
using System.Collections;
using UnityEngine;

namespace UnityFoundation.Code
{

    public class AsyncProcessor : Singleton<AsyncProcessor>, IAsyncProcessor
    {
        public void ProcessAsync(Action action, float startTime)
        {
            StartCoroutine(Callback(action, startTime));
        }

        private IEnumerator Callback(Action action, float startTime)
        {
            yield return new WaitForSecondsRealtime(startTime);

            action();
        }
    }
}
