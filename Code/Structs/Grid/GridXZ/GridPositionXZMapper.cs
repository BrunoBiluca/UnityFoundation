using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class GridPositionXZMapper
    {
        public static Vector3 ToVector3<TValue>(GridPositionXZ<TValue> position)
        {
            return new Vector3(position.X, 0f, position.Z);
        }
    }
}