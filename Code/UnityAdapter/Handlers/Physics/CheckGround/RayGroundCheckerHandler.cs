using System;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.Code.UnityAdapter
{
    public class RayGroundCheckerHandler : ICheckGroundHandler
    {
        public bool IsGrounded { get; private set; }
        public ICollider Collider { get; private set; }
        public float GroundOffset { get; }

        public event Action OnLanded;

        private bool previousState;
        private ITimer timer;

        public RayGroundCheckerHandler(ICollider checkedCollider, float groundOffset)
        {
            GroundOffset = groundOffset;
            Collider = checkedCollider;
        }

        public bool CheckGround()
        {
            if(timer != null && !timer.Completed)
                return false;

            IsGrounded = Physics.Raycast(
                Collider.Bounds.center,
                Collider.Transform.Down,
                Collider.Height / 2 + GroundOffset
            );

            if(!previousState && IsGrounded)
            {
                OnLanded?.Invoke();
            }
            previousState = IsGrounded;

            return IsGrounded;
        }

        public void Disable(ITimer timer)
        {
            IsGrounded = false;
            this.timer = timer;
            timer.Start();
        }
    }
}