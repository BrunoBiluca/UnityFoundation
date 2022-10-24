using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

namespace UnityFoundation.Code.UnityAdapter
{
    public class RaycastHandler : IRaycastHandler
    {
        private readonly ICamera camera;

        public RaycastHandler(ICamera camera)
        {
            this.camera = camera;
        }

        public T GetObjectOf<T>(Vector2 screenPoint, LayerMask layerMask)
        {
            if(!TryHit(screenPoint, out RaycastHit hit, layerMask))
                return default;

            hit.collider.TryGetComponent(out T target);
            return target;
        }

        public bool TryHit(Vector2 position, out RaycastHit hit, LayerMask layerMask)
        {
            var ray = camera.ScreenPointToRay(position);

            if(!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                return false;

            return true;
        }

        public Optional<Vector3> GetWorldPosition(Vector3 screenPoint, LayerMask layerMask)
        {
            if(TryHit(screenPoint, out RaycastHit hit, layerMask))
                return Optional<Vector3>.Some(hit.point);

            return Optional<Vector3>.None();
        }
    }
}
