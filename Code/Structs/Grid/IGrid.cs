using System;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public interface IGrid<TValue>
    {
        int Width { get; }
        int Depth { get; }
        int CellSize { get; }

        Vector3 GetWorldPosition(int x, int y);

        Vector3 GetWorldPosition(int x, int y, TValue value);

        bool TrySetGridValue(Vector3 position, TValue value);

        bool ClearGridValue(Vector3 position);

        [Obsolete]
        bool ClearGridValue(TValue value);
        void Fill(TValue value);
    }

    public interface IGridXZ<TValue> : IGrid<TValue>
    {
        GridPositionXZ<TValue>[,] GridArray { get; }
        IntXZ GetGridPosition(int x, int z);
        bool TrySetValue(int x, int z, TValue value);
        TValue GetValue(int x, int z);
        bool CanSetGridValue(IntXZ gridPosition, TValue value);
        bool IsInsideGrid(int x, int z);
    }
}
