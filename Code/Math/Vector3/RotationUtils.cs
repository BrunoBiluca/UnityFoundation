using UnityEngine;

namespace UnityFoundation.Code
{
    public static class RotationUtils
    {

        public static Vector3 GetZRotation(Vector3 direction)
        {
            return new Vector3(
                0,
                0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
            );
        }

        public static void RotateZ(this Transform transform, Vector3 start, Vector3 end)
        {
            transform.Rotate(GetZRotation(end - start));
        }

    }
}