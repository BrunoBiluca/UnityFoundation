using System;
using UnityEngine;
using UnityFoundation.Code.Math;
using UnityFoundation.Code.Timer;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public class SphereGroundChecker : ICheckGroundHandler
    {
        public bool IsGrounded { get; private set; }

        public ICollider Collider => throw new NotSupportedException();

        public float GroundOffset { get; set; }
        public float GroundRadius { get; set; }
        public LayerMask GroundLayers { get; set; }
        public ITransform Transform { get; }

        public event Action OnLanded;

        public SphereGroundChecker(ITransform transform)
        {
            Transform = transform;
        }

        public bool CheckGround()
        {
            var spherePosition = Transform.Position.WithYOffset(-GroundOffset);

            IsGrounded = Physics.CheckSphere(
                spherePosition,
                GroundRadius,
                GroundLayers,
                QueryTriggerInteraction.Ignore
            );

            return IsGrounded;
        }

        public void Disable(ITimer timer)
        {
            throw new NotImplementedException();
        }
    }
}
