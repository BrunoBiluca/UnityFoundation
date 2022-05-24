using System;
using UnityFoundation.Code.TimeUtils;

namespace UnityFoundation.Code.PhysicsUtils
{
    public interface ICheckGroundHandler
    {
        bool IsGrounded { get; }

        event Action OnLanded;

        bool CheckGround();
        ICheckGroundHandler DebugMode(bool active);
        void Disable(ITimer timer);
    }
}