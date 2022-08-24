using UnityEngine;

namespace UnityFoundation.Code
{
    public interface IGameObject
    {
        string Name { get; }
        bool IsActiveInHierarchy { get; }
    }
}
