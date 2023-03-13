using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Physics3D.CheckGround
{
    public class DebugGroundCheckerTestFixture : BaseCheckGroundTestFixture
    {
        public override ICheckGroundHandler Build()
        {
            var checker = new CheckGroundHandlerTestFixture() {
                Collider = Collider,
                GroundedOffset = GroundedOffset
            };
            return new DebugGroundChecker(checker.Build());
        }
    }
}