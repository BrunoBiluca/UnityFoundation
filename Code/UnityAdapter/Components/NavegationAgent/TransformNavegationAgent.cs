using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class TransformNavegationAgent : INavegationAgent
    {
        private readonly ITransform transform;
        private Optional<Vector3> target;
        private readonly float rotateSpeed;

        public event Action OnReachDestination;

        public TransformNavegationAgent(ITransform transform)
        {
            this.transform = transform;

            target = Optional<Vector3>.None();
            Speed = 1f;
            rotateSpeed = 10f;
        }

        public Vector3 CurrentPosition => transform.Position;

        public float PositionInterpolation { get; private set; }
        public float Speed { get; set; }
        public float StoppingDistance { get; set; }
        public float RemainingDistance => DistanceMagnitude();

        public void Disabled()
        {
            throw new NotImplementedException();
        }

        public void ResetPath()
        {
            target = Optional<Vector3>.None();
        }

        public bool SetDestination(Vector3 targetPosition)
        {
            target = Optional<Vector3>.Some(targetPosition);
            return true;
        }

        public void Update(float updateTime = 1f)
        {
            if(!target.IsPresentAndGet(out Vector3 destination)) return;

            var moveDirection = destination - CurrentPosition;
            transform.Position += Speed * updateTime * moveDirection.normalized;

            transform.Forward = Vector3.Lerp(
                transform.Forward, moveDirection, updateTime * rotateSpeed
            );

            if(DistanceMagnitude() <= StoppingDistance)
            {
                ResetPath();
                OnReachDestination?.Invoke();
                return;
            }
        }

        private float DistanceMagnitude()
        {
            var pos = target.OrElse(CurrentPosition);
            return (pos - CurrentPosition).magnitude;
        }
    }
}