using UnityEngine;

namespace Assets.UnityFoundation.CameraScripts {
    public interface IFollowable {
        Vector3 GetPosition();
        Vector3 GetPositionOffset();
        bool StopFollow();
    }
}
