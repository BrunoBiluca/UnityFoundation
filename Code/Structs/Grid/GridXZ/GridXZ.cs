using System;
using System.Collections.Generic;

namespace UnityFoundation.Code.Grid
{
    public partial class GridXZ<TValue> : IGridXZ<TValue>
    {
        private readonly GridCellXZ<TValue>[,] gridArray;

        public int Width { get; private set; }
        public int Depth { get; private set; }
        public int CellSize { get; private set; }
        public GridCellXZ<TValue>[,] Cells => gridArray;

        public bool ForceSetValue { get; set; } = false;

        public GridXZ(
            int width,
            int depth,
            int cellSize
        )
        {
            Width = width;
            Depth = depth;
            CellSize = cellSize;

            gridArray = new GridCellXZ<TValue>[width, depth];
            for(int x = 0; x < width; x++)
            {
                for(int z = 0; z < depth; z++)
                {
                    var newPos = new GridCellXZ<TValue>(x, z);
                    newPos.SetIndex(GetGridIndex(x, z));
                    gridArray[x, z] = newPos;
                }
            }
        }

        private int GetGridIndex(int x, int z)
        {
            return x * Width + z;
        }

        public bool TrySetValue(int x, int z, TValue value)
        {
            var gridPosition = MapToGridPosition(x, z);
            if(!CanSetGridValue(gridPosition))
                return false;

            SetValue(gridPosition, value);
            return true;
        }

        public TValue GetValue(int x, int z)
        {
            return GetValue(MapToGridPosition(x, z));
        }

        public void Fill(TValue value)
        {
            foreach(var gridPos in Cells)
                gridPos.Value = value;
        }

        public virtual bool IsInsideGrid(int x, int z)
        {
            return IsInsideGrid(MapToGridPosition(x, z));
        }

        public virtual bool ClearValue(TValue value)
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Depth; y++)
                {
                    if(gridArray[x, y].Value == null)
                        continue;
                    if(!gridArray[x, y].Value.Equals(value))
                        continue;

                    ClearValue(x, y);
                }
            }

            return true;
        }

        public void ClearValue(int x, int z)
        {
            SetValueDefault(MapToGridPosition(x, z));
        }

        public bool CanSetGridValue(int x, int z)
        {
            return CanSetGridValue(MapToGridPosition(x, z));
        }

        public bool TryUpdateValue(int x, int z, Action<TValue> updateCallback)
        {
            return TryUpdateValue(MapToGridPosition(x, z), updateCallback);
        }

        public (int x, int z) GetCellPosition(int x, int z)
        {
            return MapToScaled(MapToGridPosition(x, z));
        }

        // --------------------------------------------------------------------------
        // --------------             PROTECTED METHODS               ---------------
        // --------------------------------------------------------------------------

        protected bool IsValueEmpty(TValue value)
        {
            return EqualityComparer<TValue>.Default.Equals(value, default);
        }

        // --------------------------------------------------------------------------
        // --------------              PRIVATE METHODS                ---------------
        // --------------------------------------------------------------------------
        private GridPositionXZ MapToGridPosition(int x, int z)
        {
            var cellPos = new GridPositionXZ(x / CellSize, z / CellSize);

            if(!IsInsideGrid(cellPos))
                throw new ArgumentOutOfRangeException("Position out of grid");

            return cellPos;
        }

        private (int x, int z) MapToScaled(GridPositionXZ gridPos)
        {
            return (gridPos.X * CellSize, gridPos.Z * CellSize);
        }

        private bool IsInsideGrid(GridPositionXZ cellPos)
        {
            return cellPos.X >= 0 && cellPos.X < Width
                && cellPos.Z >= 0 && cellPos.Z < Depth;
        }

        private void SetValueDefault(GridPositionXZ cellPos)
        {
            SetValue(cellPos, default);
        }

        private void SetValue(GridPositionXZ cellPos, TValue value)
        {
            gridArray[cellPos.X, cellPos.Z].Value = value;
        }

        private TValue GetValue(GridPositionXZ cellPos)
        {
            return gridArray[cellPos.X, cellPos.Z].Value;
        }

        private bool CanSetGridValue(GridPositionXZ cellPos)
        {
            var gridValue = gridArray[cellPos.X, cellPos.Z].Value;
            if(!ForceSetValue && !IsValueEmpty(gridValue))
                return false;

            return true;
        }

        private bool TryUpdateValue(GridPositionXZ cellPos, Action<TValue> updateCallback)
        {
            var value = GetValue(cellPos.X, cellPos.Z);

            if(IsValueEmpty(value))
                return false;

            updateCallback(value);
            return true;
        }
    }
}
