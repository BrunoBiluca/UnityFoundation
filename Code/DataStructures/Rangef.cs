using System;
using UnityEngine;

namespace UnityFoundation.Code
{
    [Serializable]
    public class Rangef
    {
        [SerializeField] private float start;
        [SerializeField] private float end;

        public float Start { get { return start; } }
        public float End { get { return end; } }

        public Rangef() { }

        public Rangef(float start, float end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
