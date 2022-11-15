using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFoundation.Code
{
    [Obsolete("Use instead RaycastHandler")]
    public static class RaycastHelper
    {
        private static Camera mainCamera;

        public static bool Raycast(Vector2 position, out RaycastHit hit, LayerMask layerMask)
        {
            if(mainCamera == null) mainCamera = Camera.main;

            Ray ray = mainCamera.ScreenPointToRay(position);

            if(!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                return false;

            return true;
        }

        public static bool RaycastType<T>(Vector2 position, out T target, LayerMask layerMask)
        {
            if(!Raycast(position, out RaycastHit hit, layerMask))
            {
                target = default;
                return false;
            }

            if(!hit.collider.TryGetComponent(out target))
                return false;

            return true;
        }

        public static bool RaycastMousePosition(out RaycastHit target, LayerMask layerMask)
        {
            var position = Mouse.current.position.ReadValue();
            return Raycast(position, out target, layerMask);
        }

        public static bool RaycastTypeMousePosition<T>(out T target, LayerMask layerMask)
        {
            var position = Mouse.current.position.ReadValue();
            return RaycastType(position, out target, layerMask);
        }
    }
}