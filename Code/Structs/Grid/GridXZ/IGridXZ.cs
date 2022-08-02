namespace UnityFoundation.Code.Grid
{
    public interface IGridXZ<TValue>
    {
        int Width { get; }
        int Depth { get; }
        int CellSize { get; }

        GridPositionXZ<TValue>[,] GridMatrix { get; }
        bool CanSetGridValue(IntXZ gridPosition, TValue value);
        IntXZ GetCellPosition(int x, int z);
        TValue GetValue(int x, int z);
        bool IsInsideGrid(int x, int z);
        bool TrySetValue(int x, int z, TValue value);
        void Fill(TValue value);
    }
}
