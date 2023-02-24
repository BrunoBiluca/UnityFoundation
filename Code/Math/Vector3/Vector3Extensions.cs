using UnityEngine;

namespace UnityFoundation.Code
{
    public static class Vector3Extensions
    {
        public static Vector3 WithY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }
    }
}