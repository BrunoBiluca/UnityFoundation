namespace UnityFoundation.Code.Grid
{
    public interface IGridXZ<TValue> : IGrid<TValue>
    {
        GridPositionXZ<TValue>[,] GridMatrix { get; }
        bool CanSetGridValue(IntXZ gridPosition, TValue value);
        IntXZ GetGridPosition(int x, int z);
        TValue GetValue(int x, int z);
        bool IsInsideGrid(int x, int z);
        bool TrySetValue(int x, int z, TValue value);
    }
}
