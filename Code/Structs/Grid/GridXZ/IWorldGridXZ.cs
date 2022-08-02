using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public interface IWorldGridXZ<T> : IGridXZ<T>
    {
        void ClearValue(Vector3 position);
        Vector3 GetCellWorldPosition(Vector3 worldPosition);
        T GetValue(Vector3 worldPosition);
        bool TrySetValue(Vector3 worldPosition, T value);
    }
}
