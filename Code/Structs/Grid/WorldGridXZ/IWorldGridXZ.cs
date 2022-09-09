using System;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{

    public interface IWorldGridXZ<T>
    {
        Vector3 InitialPosition { get; }
        Vector3 WidthPosition { get; }
        Vector3 DepthPosition { get; }
        Vector3 WidthAndDepthPosition { get; }
        int CellSize { get; }
        int Width { get; }
        int Depth { get; }
        GridCellXZ<T>[,] Cells { get; }

        void Fill(T value);
        void ClearValue(Vector3 position);
        Vector3 GetCellCenterPosition(Vector3 worldPosition);
        Vector3 GetCellWorldPosition(Vector3 worldPosition);
        T GetValue(Vector3 worldPosition);
        bool TrySetValue(Vector3 worldPosition, T value);
        bool TryUpdateValue(Vector3 position, Action<T> updateCallback);
    }
}
