using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class RagdollHandler : MonoBehaviour
    {
        [SerializeField] private Transform ragdollRoot;

        public ITransform Root { get; private set; }

        public void Awake()
        {
            Root = new TransformDecorator(ragdollRoot);
        }

        public void Setup(ITransform originalRoot)
        {
            MatchAllTransforms(Root, originalRoot);
            ApplyForce(Root, transform.position, 300f, 10f);
        }

        private void MatchAllTransforms(ITransform clone, ITransform target)
        {
            foreach(ITransform child in target.GetChildren())
            {
                var originalChild = clone.Find(child.Name);

                if(originalChild == null)
                    continue;

                child.Position = originalChild.Position;
                child.Rotation = originalChild.Rotation;

                MatchAllTransforms(child, originalChild);
            }
        }

        private void ApplyForce(ITransform target, Vector3 position, float force, float radius)
        {
            foreach(ITransform child in target.GetChildren())
            {
                if(child.TryGetComponent(out Rigidbody rb))
                    rb.AddExplosionForce(force, position, radius);

                ApplyForce(child, position, force, radius);
            }
        }
    }
}
