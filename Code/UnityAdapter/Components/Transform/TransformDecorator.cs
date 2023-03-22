using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class TransformDecorator : ITransform
    {
        private readonly UnityObjectRef<Transform> comp;

        public bool IsValid => comp.IsValid;

        public event Action OnInvalidState;

        public TransformDecorator(Transform transform)
        {
            comp = new UnityObjectRef<Transform>(transform);
            comp.OnInvalidState += () => OnInvalidState?.Invoke();
        }

        public Vector3 Forward {
            get => comp.Ref(t => t.forward);
            set => comp.Ref(t => t.forward = value);
        }
        public Vector3 Right {
            get => comp.Ref(t => t.right);
            set => comp.Ref(t => t.right = value);
        }

        public Vector3 Down {
            get => comp.Ref(t => t.Down());
            set => throw new NotSupportedException();
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

        public void SetRotation(Vector3 rotation)
        {
            comp.Ref(t => t.eulerAngles = rotation);
        }

        public void RotateOnWorld(Vector3 rotation)
        {
            comp.Ref(t => t.Rotate(rotation, Space.World));
        }
    }
}
