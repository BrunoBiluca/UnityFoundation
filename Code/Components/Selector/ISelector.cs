using UnityEngine;

namespace UnityFoundation.Code
{
    public interface ISelector
    {
        Optional<ISelectable> CurrentUnit { get; }
        Optional<ISelectable> Select(Vector3 screenPosition);
        Optional<T> Select<T>(Vector3 screenPosition) where T : ISelectable;
        void Unselect();
    }
}