using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public interface IGameObject : IComponentState
    {
        string Name { get; }
        bool IsActiveInHierarchy { get; }

        void SetActive(bool state);
    }
}
