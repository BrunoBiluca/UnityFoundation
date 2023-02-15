using UnityEngine;

namespace UnityFoundation.Code
{
    public static class LinearInterpolation
    {
        public static Vector3 Quadratic(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            var ab = Vector3.Lerp(a, b, t);
            var bc = Vector3.Lerp(b, c, t);

            return Vector3.Lerp(ab, bc, t);
        }

        public static Vector3 Cubic(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            var abc = Quadratic(a, b, c, t);
            var bcd = Quadratic(b, c, d, t);

            return Vector3.Lerp(abc, bcd, t);
        }
    }
}