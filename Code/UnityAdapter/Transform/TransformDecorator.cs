using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class TransformDecorator : ITransform
    {
        private readonly UnityComponentRef<Transform> comp;

        public bool IsValid => comp.IsValid;

        public event Action OnInvalidState;

        public TransformDecorator(Transform transform)
        {
            comp = new UnityComponentRef<Transform>(transform);
            comp.OnInvalidState += () => OnInvalidState?.Invoke();
        }

        public Vector3 Foward {
            get => comp.Ref(t => t.forward);
            set => comp.Ref(t => t.forward = value);
        }

        public Vector3 Position {
            get => comp.Ref(t => t.position);
            set => comp.Ref(t => t.position = value);
        }

        public string Name {
            get => comp.Ref(t => t.name);
            set => comp.Ref(t => t.name = value);
        }
        public Quaternion Rotation {
            get => comp.Ref(t => t.rotation);
            set => comp.Ref(t => t.rotation = value);
        }

        public ITransform GetTransform()
        {
            return this;
        }

        public void Rotate(Vector3 eulers)
        {
            comp.Ref(t => t.Rotate(eulers));
        }

        public void LookAt(Vector3 position)
        {
            comp.Ref(t => t.LookAt(position));
        }

        public IEnumerable<ITransform> GetChildren()
        {
            foreach(var c in comp.Ref(t => t.GetChildren()))
                yield return new TransformDecorator(c);
        }

        public ITransform Find(string name)
        {
            return comp.Ref(t => new TransformDecorator(t.Find(name)));
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            return comp.Ref(t => t).TryGetComponent(out component);
        }
    }
}
