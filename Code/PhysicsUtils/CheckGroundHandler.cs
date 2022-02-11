using System;
using UnityEngine;

namespace Assets.UnityFoundation.Code.PhysicsUtils
{
    public class CheckGroundHandler
    {
        public bool IsGrounded { get; private set; }

        public event Action OnLanded;

        private bool debugMode;
        private readonly Transform transform;
        private readonly Collider collider;
        private float groundOffset;
        private bool previousState;

        public CheckGroundHandler(Transform checkedTransform, float groundOffset)
        {
            transform = checkedTransform;
            this.groundOffset = groundOffset;
            collider = checkedTransform.gameObject.GetComponent<Collider>();
        }

        public CheckGroundHandler DebugMode(bool active)
        {
            debugMode = active;
            return this;
        }

        public bool CheckGround()
        {
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
    }
}