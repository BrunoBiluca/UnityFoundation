using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public interface IWorldGridXZ<T> : IGridXZ<T>
    {
        Vector3 InitialPosition { get; }
        Vector3 WidthPosition { get; }
        Vector3 DepthPosition { get; }
        Vector3 WidthAndDepthPosition { get; }

        void ClearValue(Vector3 position);
        Vector3 GetCellCenterPosition(Vector3 worldPosition);
        Vector3 GetCellWorldPosition(Vector3 worldPosition);
        Vector3 GetCellWorldPosition(IntXZ gridPosition);
        T GetValue(Vector3 worldPosition);
        bool TrySetValue(Vector3 worldPosition, T value);
    }
}
