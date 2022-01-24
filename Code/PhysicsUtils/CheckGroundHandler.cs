using UnityEngine;

namespace Assets.UnityFoundation.Code.PhysicsUtils
{
    public class CheckGroundHandler
    {
        public bool IsGrounded { get; private set; }

        private bool debugMode;
        private Transform transform;
        private Collider collider;

        public CheckGroundHandler(Transform checkedTransform)
        {
            this.transform = checkedTransform;
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
                collider.height + 0.05f
            );

            if(debugMode)
            {
                Debug.DrawRay(
                    collider.bounds.center,
                    transform.Down() * (collider.height + 0.05f),
                    IsGrounded ? Color.green : Color.red
                );
            }
        }
    }
}