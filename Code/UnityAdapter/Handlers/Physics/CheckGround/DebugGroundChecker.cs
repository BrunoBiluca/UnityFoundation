using System;
using UnityEngine;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.Code.UnityAdapter
{
    public class DebugGroundChecker : ICheckGroundHandler
    {
        private readonly ICheckGroundHandler checkGroundHandler;
        private RayDrawer debugDrawer;

        public DebugGroundChecker(ICheckGroundHandler checkGroundHandler)
        {
            this.checkGroundHandler = checkGroundHandler;
            checkGroundHandler.OnLanded += () => OnLanded?.Invoke();

            debugDrawer = new RayDrawer();
        }

        public bool IsGrounded => checkGroundHandler.IsGrounded;

        public ICollider Collider => checkGroundHandler.Collider;

        public float GroundOffset => checkGroundHandler.GroundOffset;

        public event Action OnLanded;

        public bool CheckGround()
        {
            ICollider col = checkGroundHandler.Collider;
            debugDrawer.Start = col.Bounds.center;
            debugDrawer.Direction = col.Transform.Down
                * (col.Height / 2 + checkGroundHandler.GroundOffset);
            debugDrawer.Color = IsGrounded ? Color.green : Color.red;
            return checkGroundHandler.CheckGround();
        }

        public void Disable(ITimer timer) => checkGroundHandler.Disable(timer);
    }
}