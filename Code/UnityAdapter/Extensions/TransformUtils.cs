using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public static class TransformUtils
    {

        public static bool IsInRange(Transform target, Transform pursuer, float range)
        {
            float distance = (
                target.transform.position - pursuer.position
            ).sqrMagnitude;
            return distance < Mathf.Pow(range, 2);
        }

        public static void ChangeLayer(Transform target, LayerMask layer)
        {
            foreach(Transform child in target)
            {
                child.gameObject.layer = layer;
            }
        }

        public static T FindComponent<T>(Transform transform, params string[] transformNames)
        {
            var auxTransform = transform;
            foreach(var name in transformNames)
            {
                auxTransform = auxTransform.Find(name);

                if(auxTransform == null)
                    return default;
            }

            return auxTransform.GetComponent<T>();
        }

        public static T[] FindComponentsInChildren<T>(
            Transform transform, params string[] transformNames
        ) where T : Component
        {
            var parent = FindComponent<Transform>(transform, transformNames);

            if(parent == null)
                return default;

            var childrenComponents = new List<T>();
            foreach(Transform child in parent)
            {
                if(child.TryGetComponent(out T comp))
                    childrenComponents.Add(comp);
            }
            return childrenComponents.ToArray();
        }

        public static void RemoveChildObjects(Transform parent)
        {
            var children = new List<GameObject>();

            foreach(Transform child in parent)
            {
                children.Add(child.gameObject);
            }

            foreach(var child in children)
            {
                Object.Destroy(child);
            }
        }

        public static IEnumerator RemoveChildObjects(Transform parent, float waitBetween)
        {
            var children = new List<GameObject>();

            foreach(Transform child in parent)
            {
                children.Add(child.gameObject);
            }

            foreach(var child in children)
            {
                Object.Destroy(child);
                yield return new WaitForSecondsRealtime(waitBetween);
            }
        }
    }
}