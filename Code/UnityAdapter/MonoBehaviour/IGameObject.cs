using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public interface IGameObject : IValidState
    {
        string Name { get; }
        bool IsActiveInHierarchy { get; }

        void SetActive(bool state);
    }
}
