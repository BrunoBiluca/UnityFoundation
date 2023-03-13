using System;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface ICheckGroundHandler
    {
        bool IsGrounded { get; }
        ICollider Collider { get; }
        float GroundOffset { get; }

        event Action OnLanded;

        bool CheckGround();
        void Disable(ITimer timer);
    }
}