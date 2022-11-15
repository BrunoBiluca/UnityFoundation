using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IRaycastHandler
    {
        T GetObjectOf<T>(Vector2 screenPosition, LayerMask layerMask);
        Optional<Vector3> GetWorldPosition(Vector3 screenPoint, LayerMask layerMask);
        bool TryHit(Vector2 position, out RaycastHit hit, LayerMask layerMask);
    }
}
