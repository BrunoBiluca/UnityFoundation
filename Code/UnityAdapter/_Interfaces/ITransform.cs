using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{

    public interface ITransform : IComponentState
    {
        string Name { get; set; }
        Vector3 Forward { get; set; }
        Vector3 Right { get; set; }
        Vector3 Down { get; set; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        void Rotate(Vector3 eulers);
        void RotateOnWorld(Vector3 vector3);
        ITransform GetTransform();
        void LookAt(Vector3 position);
        void SetRotation(Vector3 rotation);
        IEnumerable<ITransform> GetChildren();
        ITransform Find(string name);
        bool TryGetComponent<T>(out T component) where T : Component;
    }
}
