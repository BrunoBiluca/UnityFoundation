using Assets.UnityFoundation.TimeUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Code {
    public static class TransformUtils {

        public static bool IsInRange(Transform target, Transform pursuer, float range)
        {
            float distance = (target.transform.position - pursuer.position).sqrMagnitude;
            return distance < Mathf.Pow(range, 2);
        }

        public static void ChangeLayer(Transform target, LayerMask layer)
        {
            foreach(Transform child in target) {
                child.gameObject.layer = layer;
            }
        }

        public static void RemoveChildObjects(Transform parent) {
            var children = new List<GameObject>();

            foreach(Transform child in parent) {
                children.Add(child.gameObject);
            }

            foreach(var child in children) {
                Object.Destroy(child);
            }
        }

        public static IEnumerator RemoveChildObjects(Transform parent, float waitBetween) {
            var children = new List<GameObject>();

            foreach(Transform child in parent) {
                children.Add(child.gameObject);
            }

            foreach(var child in children) {
                Object.Destroy(child);
                yield return WaittingCoroutine.RealSeconds(waitBetween);
            }
        }

    }
}