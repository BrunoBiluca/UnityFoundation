using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Tools.Spline
{
    public class SplineMono : MonoBehaviour
    {
        [SerializeField] private bool closedLoop;

        [SerializeField] private List<SplineAnchor> anchors = new List<SplineAnchor>();

        public event Action OnSplineUpdated;
        public List<SplineAnchor> Anchors => anchors;
        public bool ClosedLoop {
            get => closedLoop;
            set {
                closedLoop = value;
                OnSplineUpdated?.Invoke();
            }
        }

        public void AddAnchor()
        {
            if(anchors == null) anchors = new List<SplineAnchor>();

            var newPosition = new Vector3(0, 0, 0);
            if(anchors.Count > 0)
            {
                var lastAnchor = anchors[anchors.Count - 1];
                newPosition = lastAnchor.Origin.Position + new Vector3(1, 1, 0);
            }

            anchors.Add(new SplineAnchor(
                new SplinePoint(newPosition),
                new SplinePoint(newPosition + new Vector3(1, 0, 0)),
                new SplinePoint(newPosition + new Vector3(-1, 0, 0))
            ));

            OnSplineUpdated?.Invoke();
        }

        public Vector3 GetPosition(float interpolateAmount)
        {
            var segments = anchors.Count - 1;
            if(closedLoop) segments += 1;

            var interpolate = Mathf.Clamp01(interpolateAmount) * segments;

            var currentAnchor = Mathf.FloorToInt(interpolate);
            var nextAnchor = currentAnchor + 1;

            // Last segment
            if(currentAnchor >= anchors.Count - 1)
            {
                currentAnchor = anchors.Count - 2;
                nextAnchor = currentAnchor + 1;

                if(closedLoop)
                {
                    currentAnchor = anchors.Count - 1;
                    nextAnchor = 0;
                }
            }

            var anchorA = anchors[currentAnchor];
            var anchorB = anchors[nextAnchor];

            var currentInterpolateAmount = interpolate - currentAnchor;
            return LinearInterpolation.Cubic(
                GetPosition(anchorA.Origin),
                GetPosition(anchorA.PointB),
                GetPosition(anchorB.PointA),
                GetPosition(anchorB.Origin),
                currentInterpolateAmount
            );
        }

        public Vector3 GetPosition(SplinePoint point)
        {
            return transform.position + point.Position;
        }

        public void SetPointPosition(SplinePoint point, Vector3 newPosition)
        {
            point.Position = newPosition - transform.position;
            OnSplineUpdated?.Invoke();
        }
    }
}