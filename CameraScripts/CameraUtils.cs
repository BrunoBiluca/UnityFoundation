using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.CameraScripts {
    public static class CameraUtils {
        private static Camera mainCamera;

        public static Vector3 GetMousePosition() {
            if(mainCamera == null) mainCamera = Camera.main;

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            return worldPosition;
        }
    }
}