using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class CapsuleColliderDecorator : ICollider
    {
        private readonly UnityObjectRef<CapsuleCollider> comp;

        public CapsuleColliderDecorator(CapsuleCollider collider)
        {
            comp = new UnityObjectRef<CapsuleCollider>(collider);
            Transform = collider.transform.Decorate();
        }

        public ITransform Transform { get; private set; }

        public Bounds Bounds {
            get => comp.Ref(c => c.bounds);
        }
        public float Height {
            get => comp.Ref(c => c.height);
            set => comp.Ref(c => c.height = value);
        }
    }
}
