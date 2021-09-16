using UnityEngine;

namespace Assets.UnityFoundation.CameraScripts
{
    public static class CameraUtils
    {
        private static Camera mainCamera;

        public static Vector3 GetMousePosition2D()
        {
            if(mainCamera == null) mainCamera = Camera.main;

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            return worldPosition;
        }

        public static Vector3 GetMousePosition2D(Camera camera)
        {
            var worldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            return worldPosition;
        }

        public static Vector3 GetMousePosition3D()
        {
            if(mainCamera == null) mainCamera = Camera.main;

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
                return hit.point;

            return Vector3.zero;
        }
    }
}