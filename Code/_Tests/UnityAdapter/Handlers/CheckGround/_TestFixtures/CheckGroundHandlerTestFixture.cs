using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Physics3D.CheckGround
{
    public class CheckGroundHandlerTestFixture : BaseCheckGroundTestFixture
    {
        public override ICheckGroundHandler Build()
        {
            return new RayGroundCheckerHandler(Collider, GroundedOffset);
        }
    }
}