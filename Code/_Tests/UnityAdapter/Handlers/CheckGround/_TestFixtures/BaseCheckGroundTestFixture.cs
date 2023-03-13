using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Physics3D.CheckGround
{
    public abstract class BaseCheckGroundTestFixture
    {
        public ICollider Collider { get; set; }
        public float GroundedOffset { get; set; }
        public abstract ICheckGroundHandler Build();
    }
}