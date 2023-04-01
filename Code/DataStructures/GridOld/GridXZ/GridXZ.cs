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

        public GridXZConfig Config { get; private set; }

        public GridXZ(int width, int depth, int cellSize)
            : this(width, depth, cellSize, () => default)
        { }

        public GridXZ(int width, int depth, int cellSize, Func<TValue> valueFactory)
        {
            Config = new GridXZConfig() { Width = width, Depth = depth, CellSize = cellSize };
            Width = width;
            Depth = depth;
            CellSize = cellSize;

            gridArray = new GridCellXZ<TValue>[width, depth];
            for(int x = 0; x < width; x++)
            {
                for(int z = 0; z < depth; z++)
                {
                    var newPos = new GridCellXZ<TValue>(x, z, valueFactory());
                    gridArray[x, z] = newPos;
                }
            }
        }

        public GridCellXZ<TValue> GetCell(GridCellPositionScaledXZ position)
        {
            var gridPosition = MapToGridPosition(position);
            return gridArray[gridPosition.X, gridPosition.Z];
        }

        public bool TrySetValue(int x, int z, TValue value)
        {
            return TrySetValue(new GridCellPositionScaledXZ(x, z), value);
        }

        public bool TrySetValue(GridCellPositionScaledXZ position, TValue value)
        {
            var gridPosition = MapToGridPosition(position);
            if(!CanSetGridValue(gridPosition))
                return false;

            SetValue(gridPosition, value);
            return true;
        }

        public TValue GetValue(int x, int z)
        {
            return GetValue(new GridCellPositionScaledXZ(x, z));
        }

        public TValue GetValue(GridCellPositionScaledXZ position)
        {
            return GetValue(MapToGridPosition(position));
        }

        public void Fill(TValue value)
        {
            foreach(var gridPos in Cells)
                gridPos.Value = value;
        }

        public virtual bool IsInsideGrid(GridCellPositionScaledXZ position)
        {
            return IsInsideGrid(MapToGridPosition(position));
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

                    ClearValue(MapToScaled(new GridCellPositionXZ(x, y)));
                }
            }

            return true;
        }

        public void ClearValue(int x, int z)
        {
            ClearValue(new GridCellPositionScaledXZ(x, z));
        }

        public void ClearValue(GridCellPositionScaledXZ position)
        {
            SetValueDefault(MapToGridPosition(position));
        }

        public bool CanSetGridValue(GridCellPositionScaledXZ position)
        {
            return CanSetGridValue(MapToGridPosition(position));
        }

        public bool TryUpdateValue(int x, int z, Action<TValue> updateCallback)
        {
            return TryUpdateValue(new GridCellPositionScaledXZ(x, z), updateCallback);
        }

        public bool TryUpdateValue(GridCellPositionScaledXZ position, Action<TValue> updateCallback)
        {
            return TryUpdateValue(MapToGridPosition(position), updateCallback);
        }

        public GridCellPositionScaledXZ GetCellPosition(int x, int z)
        {
            return GetCellPosition(new GridCellPositionScaledXZ(x, z));
        }

        public GridCellPositionScaledXZ GetCellPosition(GridCellPositionScaledXZ position)
        {
            return MapToScaled(MapToGridPosition(position));
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
        private GridCellPositionXZ MapToGridPosition(GridCellPositionScaledXZ position)
        {
            var cellPos = new GridCellPositionXZ(position.X / CellSize, position.Z / CellSize);

            if(!IsInsideGrid(cellPos))
                throw new ArgumentOutOfRangeException("Position out of grid");

            return cellPos;
        }

        private GridCellPositionScaledXZ MapToScaled(GridCellPositionXZ gridPos)
        {
            return new GridCellPositionScaledXZ(gridPos.X * CellSize, gridPos.Z * CellSize);
        }

        private bool IsInsideGrid(GridCellPositionXZ cellPos)
        {
            return cellPos.X >= 0 && cellPos.X < Width
                && cellPos.Z >= 0 && cellPos.Z < Depth;
        }

        private void SetValueDefault(GridCellPositionXZ cellPos)
        {
            SetValue(cellPos, default);
        }

        private void SetValue(GridCellPositionXZ cellPos, TValue value)
        {
            gridArray[cellPos.X, cellPos.Z].Value = value;
        }

        private TValue GetValue(GridCellPositionXZ cellPos)
        {
            return gridArray[cellPos.X, cellPos.Z].Value;
        }

        private bool CanSetGridValue(GridCellPositionXZ cellPos)
        {
            var gridValue = gridArray[cellPos.X, cellPos.Z].Value;
            if(!ForceSetValue && !IsValueEmpty(gridValue))
                return false;

            return true;
        }

        private bool TryUpdateValue(GridCellPositionXZ cellPos, Action<TValue> updateCallback)
        {
            var value = GetValue(cellPos);

            if(IsValueEmpty(value))
                return false;

            updateCallback(value);
            return true;
        }
    }
}
