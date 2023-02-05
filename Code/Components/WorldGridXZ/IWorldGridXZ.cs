using System;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{

    // TODO: separar o que é valor T do que é implementação sem o valor da célula
    // dessa forma podemos ter IWorldGridXZ e IWorldGridXZ<>
    // onde o IWorldGridXZ normal não tem valor contido
    public interface IWorldGridXZ<T> : IGridXZCells<T>
    {
        Vector3 InitialPosition { get; }
        Vector3 WidthPosition { get; }
        Vector3 DepthPosition { get; }
        Vector3 WidthAndDepthPosition { get; }

        GridCellXZ<T> GetCell(Vector3 worldPosition);
        void Fill(T value);
        void ClearValue(Vector3 position);

        /// <summary>
        /// Return the position based on the cell's center
        /// </summary>
        Vector3 GetCellCenterPosition(Vector3 worldPosition);

        /// <summary>
        /// Return the position based on the cell's bottom-left point
        /// </summary>
        Vector3 GetCellWorldPosition(Vector3 worldPosition);
        T GetValue(Vector3 worldPosition);
        bool TrySetValue(Vector3 worldPosition, T value);
        bool TryUpdateValue(Vector3 position, Action<T> updateCallback);

        /// <summary>
        /// Return the position based on the cell's center.
        /// Don't take into account the CellSize.
        Vector3 GetCellCenterPosition(GridCellPositionXZ position);
    }
}
