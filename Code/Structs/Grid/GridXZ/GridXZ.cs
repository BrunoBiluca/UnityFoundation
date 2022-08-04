using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class GridXZ<TValue> : IGridXZ<TValue>
    {
        protected readonly GridPositionXZ<TValue>[,] gridArray;

        public int Width { get; private set; }
        public int Depth { get; private set; }
        public int CellSize { get; private set; }
        public GridPositionXZ<TValue>[,] GridMatrix => gridArray;

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

            gridArray = new GridPositionXZ<TValue>[width, depth];
            for(int x = 0; x < width; x++)
            {
                for(int z = 0; z < depth; z++)
                {
                    var newPos = new GridPositionXZ<TValue>(x, z);
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
            var gridPosition = GetCellPosition(x, z);
            if(!CanSetGridValue(gridPosition, value))
                return false;

            SetValue(gridPosition, value);
            return true;
        }

        public virtual bool CanSetGridValue(IntXZ gridPosition, TValue value)
        {
            if(!IsInsideGrid(gridPosition.X, gridPosition.Z))
                return false;

            var gridValue = gridArray[gridPosition.X, gridPosition.Z].Value;
            if(!ForceSetValue && !IsValueEmpty(gridValue))
                return false;

            return true;
        }

        protected bool IsValueEmpty(TValue value)
        {
            return EqualityComparer<TValue>.Default.Equals(value, default);
        }

        protected void SetValueDefault(IntXZ gridPos)
        {
            SetValue(gridPos, default);
        }

        protected void SetValue(IntXZ gridPos, TValue value)
        {
            gridArray[gridPos.X, gridPos.Z].Value = value;
        }

        public TValue GetValue(int x, int z)
        {
            var gridPos = GetCellPosition(x, z);
            return gridArray[gridPos.X, gridPos.Z].Value;
        }

        public IntXZ GetCellPosition(int x, int z)
        {
            var cellX = x / CellSize;
            var cellZ = z / CellSize;

            if(!IsInsideGrid(cellX, cellZ))
                throw new ArgumentOutOfRangeException("Position out of grid");

            return new IntXZ(cellX, cellZ);
        }

        public void Fill(TValue value)
        {
            foreach(var gridPos in GridMatrix)
                gridPos.Value = value;
        }

        public virtual bool IsInsideGrid(int x, int z)
        {
            return x >= 0 && x < Width
                && z >= 0 && z < Depth;
        }

        public void ClearValue(int x, int z)
        {
            var gridPos = GetCellPosition(x, z);
            SetValueDefault(gridPos);
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

    }
}
