using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Math
{
    public static class Vector3Extensions
    {
        public static Vector3 WithY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }

        public static Vector3 WithYOffset(this Vector3 vector, float offset)
        {
            return new Vector3(vector.x, vector.y + offset, vector.z);
        }

        public static IEnumerable<Vector3> PositionsInRange(this Vector3 center, int range, float stepSize)
        {
            for(var i = -range; i <= range; i++)
            {
                for(var j = -range; j <= range; j++)
                {
                    if(Mathf.Abs(i) + Mathf.Abs(j) > range)
                        continue;
                    yield return new(center.x + i * stepSize, 0, center.z + j * stepSize);
                }
            }
        }
    }
}