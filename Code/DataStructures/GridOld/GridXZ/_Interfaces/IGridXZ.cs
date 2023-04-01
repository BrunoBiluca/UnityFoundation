using System;

namespace UnityFoundation.Code.Grid
{
    public interface IGridXZ<TValue> : IGridXZCells<TValue>
    {
        GridCellXZ<TValue> GetCell(GridCellPositionScaledXZ position);
        bool IsInsideGrid(GridCellPositionScaledXZ position);
        bool CanSetGridValue(GridCellPositionScaledXZ position);
        TValue GetValue(GridCellPositionScaledXZ position);
        bool TrySetValue(GridCellPositionScaledXZ position, TValue value);
        bool TryUpdateValue(GridCellPositionScaledXZ position, Action<TValue> updateCallback) { return false; }
        void Fill(TValue value);
        GridCellPositionScaledXZ GetCellPosition(GridCellPositionScaledXZ position);
        void ClearValue(GridCellPositionScaledXZ position);
    }
}
