using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IDebugDrawer
    {
        void Draw();
    }

    public class RayDrawer : IDebugDrawer
    {
        public Vector3 Start { get; set; }
        public Vector3 Direction { get; set; }
        public Color Color { get; set; }

        public void Draw()
        {
            Debug.DrawRay(Start, Direction, Color);
        }
    }
}
