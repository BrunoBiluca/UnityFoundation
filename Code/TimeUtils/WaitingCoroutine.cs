using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.TimeUtils
{
    public static class WaittingCoroutine
    {
        public static IEnumerator RealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;

            while(Time.realtimeSinceStartup < (start + time))
            {
                yield return null;
            }
        }

        public static IEnumerator UntilPropertyTrue<T>(
            T instance, string propertyName, float timeout = 999f
        )
        {
            float start = Time.realtimeSinceStartup;

            while(Time.realtimeSinceStartup < (start + timeout))
            {
                var value = (bool)instance.GetType()
                    .GetProperty(propertyName)
                    .GetValue(instance, null);

                if(value) break;

                yield return null;
            }
        }
    }
}