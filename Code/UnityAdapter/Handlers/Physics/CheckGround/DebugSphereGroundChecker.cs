using UnityEngine;
using UnityFoundation.Code.DebugHelper;
using UnityFoundation.Code.Math;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public class DebugSphereGroundChecker : IDebugDrawer
    {
        private readonly SphereGroundChecker groundChecker;

        public DebugSphereGroundChecker(SphereGroundChecker groundChecker)
        {
            this.groundChecker = groundChecker;
        }

        public void Draw()
        {
            var transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            var transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            DebugDraw.DrawSphere(
                groundChecker.Transform.Position.WithYOffset(-groundChecker.GroundOffset),
                groundChecker.GroundRadius,
                groundChecker.IsGrounded ? transparentGreen : transparentRed
            );
        }
    }
}
