using Unity.Mathematics;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Grid
{
    public interface IGrid<TValue>
    {
        int Width { get; }
        int Height { get; }
        int CellSize { get; }

        GridPosition<TValue>[,] GridArray { get; }

        int2 GetGridPostion(Vector3 position);

        Vector3 GetWorldPosition(int x, int y);

        Vector3 GetWorldPosition(int x, int y, TValue value);

        bool IsInsideGrid(int x, int y);

        bool CanSetGridValue(int2 gridPosition, TValue value);

        bool TrySetGridValue(Vector3 position, TValue value);
    }
}
