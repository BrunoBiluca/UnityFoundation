using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public interface ITransformUpdater
    {
        void Update(ITransform transform, float amount);
    }
}