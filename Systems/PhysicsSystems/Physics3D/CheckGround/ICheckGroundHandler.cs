using System;
using UnityFoundation.Tools.TimeUtils;

namespace UnityFoundation.Physics3D.CheckGround
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