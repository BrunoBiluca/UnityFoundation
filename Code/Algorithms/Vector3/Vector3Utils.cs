using UnityEngine;

namespace UnityFoundation.Code
{
    public class Vector3Utils
    {
        public static bool NearlyEqual(Vector3 a, Vector3 b, float delta)
        {
            return MathX.NearlyEqual(a.x, b.x, delta)
            && MathX.NearlyEqual(a.y, b.y, delta)
            && MathX.NearlyEqual(a.z, b.z, delta);
        }

        public static Vector3 RandomPoint()
        {
            return new Vector3(
                Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue)
            );
        }
    }

    public static class Vector3Extensions
    {
        public static Vector3 WithY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }
    }
}