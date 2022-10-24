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

        public Vector3 Foward {
            get => transform.forward;
            set => transform.forward = value;
        }

        public Vector3 Position {
            get => transform.position;
            set => transform.position = value;
        }

        public string Name {
            get => transform.name;
            set => transform.name = value;
        }
        public Quaternion Rotation {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public ITransform GetTransform()
        {
            return this;
        }

        public void Rotate(Vector3 eulers)
        {
            transform.Rotate(eulers);
        }
    }
}
