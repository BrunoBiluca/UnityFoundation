using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class TransformDecorator : ITransform
    {
        private readonly Transform transform;

        public TransformDecorator(Transform transform)
        {
            this.transform = transform;
        }

        public Vector3 Foward => transform.forward;

        public Vector3 Position {
            get => transform.position;
            set => transform.position = value;
        }

        public ITransform GetTransform()
        {
            return this;
        }
    }
}
