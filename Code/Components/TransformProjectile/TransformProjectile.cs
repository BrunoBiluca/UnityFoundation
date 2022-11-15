using System;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class TransformProjectile
    {
        private readonly Vector3 target;
        private readonly Vector3 direction;
        private readonly ProjectileMono proj;
        private readonly float speed;

        public event Action OnReachTarget;

        public TransformProjectile(Settings settings)
        {
            proj = settings.Projectile;
            target = settings.TargetPosition;
            direction = (settings.TargetPosition - proj.Transform.Position).normalized;
            speed = settings.Speed;
        }

        public void Update(float interpolateTime = 1.0f)
        {
            var displacement = interpolateTime * speed * direction;
            var newPosition = proj.Transform.Position + displacement;
            if(!ReachDestination(newPosition))
            {
                proj.Transform.Position = newPosition;
                return;
            }

            proj.Transform.Position = target;
            proj.Destroy();
            OnReachTarget?.Invoke();
        }

        private bool ReachDestination(Vector3 newPosition)
        {
            var beforeMoving = Vector3.Distance(proj.Transform.Position, target);
            var afterMoving = Vector3.Distance(newPosition, target);
            return beforeMoving < afterMoving;
        }

        public class Settings
        {
            public float Speed { get; set; }
            public Vector3 TargetPosition { get; set; }
            public ProjectileMono Projectile { get; set; }
        }
    }
}
