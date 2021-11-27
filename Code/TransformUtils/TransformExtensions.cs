using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UnityFoundation.Code
{
    public static class TransformExtensions
    {
        public static Transform FindTransform(this Transform transform, string path)
            => TransformUtils.FindComponent<Transform>(transform, path);

        public static T FindComponent<T>(this Transform transform, string path)
            => TransformUtils.FindComponent<T>(transform, path);
    }
}
