using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class TransformDecorator : ITransform
    {
        private readonly Transform transform;

        public bool IsValid { get; private set; }

        public event Action OnInvalidState;

        public TransformDecorator(Transform transform)
        {
            IsValid = true;
            this.transform = transform;
        }

        public Vector3 Foward {
            get => TransformRef(t => t.forward);
            set => TransformRef(t => t.forward = value);
        }

        public Vector3 Position {
            get => TransformRef(t => t.position);
            set => TransformRef(t => t.position = value);
        }

        public string Name {
            get => TransformRef(t => t.name);
            set => TransformRef(t => t.name = value);
        }
        public Quaternion Rotation {
            get => TransformRef(t => t.rotation);
            set => TransformRef(t => t.rotation = value);
        }

        public ITransform GetTransform()
        {
            return this;
        }

        public void Rotate(Vector3 eulers)
        {
            TransformRef(t => t.Rotate(eulers));
        }

        private void TransformRef(Action<Transform> call)
        {
            try
            {
                call(transform);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
            }
        }

        private T TransformRef<T>(Func<Transform, T> call)
        {
            try
            {
                return call(transform);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
                return default;
            }
        }

        private void SetInvalidState()
        {
            IsValid = false;
            OnInvalidState?.Invoke();
        }
    }
}
