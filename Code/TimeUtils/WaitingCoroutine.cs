using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.TimeUtils {
    public static class WaittingCoroutine {
        public static IEnumerator RealSeconds(float time) {
            float start = Time.realtimeSinceStartup;

            while(Time.realtimeSinceStartup < (start + time)) {
                yield return null;
            }
        }
    }
}