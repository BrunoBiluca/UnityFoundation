using System;
using UnityEngine;

namespace UnityFoundation.Tools.Spline
{
    [Serializable]
    public class SplinePoint
    {
        [SerializeField] private Vector3 position;

        public Vector3 Position { get => position; set => position = value; }

        public SplinePoint(Vector3 position)
        {
            this.position = position;
        }
    }
}