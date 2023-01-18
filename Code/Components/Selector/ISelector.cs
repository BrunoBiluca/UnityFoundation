using UnityEngine;

namespace UnityFoundation.Code
{
    public interface ISelector
    {
        Optional<ISelectable> CurrentUnit { get; }
        Optional<ISelectable> Select(Vector3 position);
        Optional<T> Select<T>(Vector3 position) where T : ISelectable;
        void Unselect();
    }
}