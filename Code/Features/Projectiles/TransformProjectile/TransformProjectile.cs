using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public class TransformProjectile : IProjectile
    {
        private Vector3 target;
        private Vector3 direction;
        private ITransform proj;
        private float speed;

        public event Action OnReachTarget;

        public void Update(float interpolateTime = 1.0f)
        {
            var displacement = interpolateTime * speed * direction;
            var newPosition = proj.Position + displacement;
            if(!ReachDestination(newPosition))
            {
                proj.Position = newPosition;
                return;
            }

            proj.Position = target;
            OnReachTarget?.Invoke();
        }

        private bool ReachDestination(Vector3 newPosition)
        {
            var beforeMoving = Vector3.Distance(proj.Position, target);
            var afterMoving = Vector3.Distance(newPosition, target);
            return beforeMoving < afterMoving;
        }

        public void Setup(IProjectile.Settings config)
        {
            proj = config.Transform;
            target = config.TargetPos;
            direction = (config.TargetPos - proj.Position).normalized;
            speed = config.Speed;
        }
    }
}
