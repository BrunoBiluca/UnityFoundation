using System;
using UnityEngine;

namespace UnityFoundation.Tools.Spline
{
    [Serializable]
    public class SplineAnchor
    {
        [SerializeField] private SplinePoint origin;
        [SerializeField] private SplinePoint anchorA;
        [SerializeField] private SplinePoint anchorB;

        public SplinePoint Origin { get => origin; set => origin = value; }
        public SplinePoint PointA { get => anchorA; set => anchorA = value; }
        public SplinePoint PointB { get => anchorB; set => anchorB = value; }

        public SplineAnchor(SplinePoint origin, SplinePoint anchorA, SplinePoint anchorB)
        {
            Origin = origin;
            PointA = anchorA;
            PointB = anchorB;
        }
    }
}