using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{

    public interface ITransform : IComponentState
    {
        string Name { get; set; }
        Vector3 Foward { get; set; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        void Rotate(Vector3 eulers);
        ITransform GetTransform();
        void LookAt(Vector3 position);
        IEnumerable<ITransform> GetChildren();
        ITransform Find(string name);
        bool TryGetComponent<T>(out T component) where T : Component;
    }
}
