using System;
using UnityEngine;
using UnityFoundation.Code.TimeUtils;

namespace UnityFoundation.Code.PhysicsUtils
{
    public class CheckGroundHandler : ICheckGroundHandler
    {
        public bool IsGrounded { get; private set; }

        public event Action OnLanded;

        private bool debugMode;
        private readonly Transform transform;
        private readonly Collider collider;
        private readonly float groundOffset;
        private bool previousState;
        private ITimer timer;

        public CheckGroundHandler(Transform checkedTransform, float groundOffset)
        {
            transform = checkedTransform;
            this.groundOffset = groundOffset;
            collider = checkedTransform.gameObject.GetComponent<Collider>();
        }

        public ICheckGroundHandler DebugMode(bool active)
        {
            debugMode = active;
            return this;
        }

        public bool CheckGround()
        {
            if(timer != null && !timer.Completed)
                return false;
                

            if(collider is CapsuleCollider capsuleCollider)
                CapsuleColliderHandler(capsuleCollider);

            return IsGrounded;
        }

        private void CapsuleColliderHandler(CapsuleCollider collider)
        {
            IsGrounded = Physics.Raycast(
                collider.bounds.center,
                transform.Down(),
                collider.height / 2 + groundOffset
            );

            if(!previousState && IsGrounded)
            {
                OnLanded?.Invoke();
            }
            previousState = IsGrounded;


            if(debugMode)
            {
                Debug.DrawRay(
                    collider.bounds.center,
                    transform.Down() * (collider.height / 2 + groundOffset),
                    IsGrounded ? Color.green : Color.red
                );
            }
        }

        public void Disable(ITimer timer)
        {
            IsGrounded = false;
            this.timer = timer;
            timer.Start();
        }
    }
}