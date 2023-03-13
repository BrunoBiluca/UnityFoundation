using System;
using UnityEngine;

namespace UnityFoundation.Code
{
    [Obsolete]
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

        public static Vector3 GetMousePosition2D(Vector2 screenPosition)
        {
            if(mainCamera == null) mainCamera = Camera.main;

            var worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
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

        public static Vector2 ScreenCenter()
            => new Vector2(Screen.width / 2f, Screen.height / 2f);

        public static Vector3 GetWorldPosition3D(Vector2 screenPosition)
        {
            if(mainCamera == null) mainCamera = Camera.main;

            var ray = mainCamera.ScreenPointToRay(screenPosition);
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
                return hit.point;

            return Vector3.zero;
        }
    }
}