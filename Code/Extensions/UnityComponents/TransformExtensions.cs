using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public static class TransformExtensions
    {
        public static Vector3 Down(this Transform transform) => transform.up * -1f;

        public static Transform FindTransform(this Transform transform, string path)
            => TransformUtils.FindComponent<Transform>(transform, path.Split('.'));

        public static Transform FindTransform(this Transform transform, params string[] path)
            => TransformUtils.FindComponent<Transform>(transform, path);

        public static T FindComponent<T>(this Transform transform, string path)
            => TransformUtils.FindComponent<T>(transform, path.Split('.'));

        public static T FindComponent<T>(this Transform transform, params string[] path)
            => TransformUtils.FindComponent<T>(transform, path);

        public static T[] FindComponentsInChildren<T>(this Transform transform, string path)
            where T : Component
            => TransformUtils.FindComponentsInChildren<T>(transform, path.Split('.'));

        public static T[] FindComponentsInChildren<T>(this Transform transform, params string[] path)
            where T : Component
            => TransformUtils.FindComponentsInChildren<T>(transform, path);

        public static float Distance(this Transform transA, Transform transB)
            => Vector3.Distance(transA.position, transB.position);

        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            foreach(Transform child in transform)
                yield return child;
        }
    }
}
