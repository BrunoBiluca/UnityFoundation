using UnityEngine;

namespace UnityFoundation.Code
{
    public class CameraPositionHelper
    {
        private readonly Camera camera;

        public CameraPositionHelper(Camera camera)
        {
            this.camera = camera;
        }

        public Optional<Vector3> GetWorldPosition(Vector3 screenPoint)
        {
            var ray = camera.ScreenPointToRay(screenPoint);
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
                return Optional<Vector3>.Some(hit.point);

            return Optional<Vector3>.None();
        }
    }
}
