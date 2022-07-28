using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class CameraDecorator : ICamera
    {
        private readonly Camera camera;

        public CameraDecorator(Camera camera)
        {
            this.camera = camera;
        }

        public Ray ScreenPointToRay(Vector2 position)
        {
            return camera.ScreenPointToRay(position);
        }
    }
}
