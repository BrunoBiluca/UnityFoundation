namespace UnityFoundation.Code.Grid
{
    public interface IGridXZBase
    {
        GridXZConfig Config { get; }
        int Width { get; }
        int Depth { get; }
        int CellSize { get; }
    }

    public interface IGridXZCells<TValue> : IGridXZBase
    {
        GridCellXZ<TValue>[,] Cells { get; }
    }
}
