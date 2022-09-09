using System;

namespace UnityFoundation.Code.Grid
{
    // TODO: alterar essa interface para receber sempre que x,z um ScaledPosition
    // O ScaledPosition será sempre a entrada dos métodos, já que o gerenciamento é interno da classe
    /// <summary>
    /// (x, z) is scaled by cell size
    /// </summary>
    public interface IGridXZ<TValue>
    {
        int Width { get; }
        int Depth { get; }
        int CellSize { get; }

        GridCellXZ<TValue>[,] Cells { get; }
        GridCellXZ<TValue> GetCell(int x, int z);
        bool IsInsideGrid(int x, int z);
        bool CanSetGridValue(int x, int z);
        TValue GetValue(int x, int z);
        bool TrySetValue(int x, int z, TValue value);
        bool TryUpdateValue(int x, int z, Action<TValue> updateCallback) { return false; }
        void Fill(TValue value);
        (int x, int z) GetCellPosition(int x, int z);
        void ClearValue(int x, int z);
    }
}
