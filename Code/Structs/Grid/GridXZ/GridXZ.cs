using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{

    public class GridXZ<TValue> : IGridXZ<TValue>
    {
        protected readonly int width;
        protected readonly int height;
        protected readonly int cellSize;
        protected readonly GridPositionXZ<TValue>[,] gridArray;

        public int Width => width;
        public int Depth => height;
        public int CellSize => cellSize;
        public GridPositionXZ<TValue>[,] GridMatrix => gridArray;

        public bool ForceSetValue { get; set; } = false;

        public GridXZ(
            int width,
            int height,
            int cellSize
        )
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridArray = new GridPositionXZ<TValue>[width, height];
            for(int x = 0; x < width; x++)
            {
                for(int z = 0; z < height; z++)
                {
                    var newPos = new GridPositionXZ<TValue>(x, z);
                    newPos.SetIndex(GetGridIndex(x, z));
                    gridArray[x, z] = newPos;
                }
            }
        }

        private int GetGridIndex(int x, int z)
        {
            return x * width + z;
        }

        public bool TrySetValue(int x, int z, TValue value)
        {
            var gridPosition = GetGridPosition(x, z);
            if(!CanSetGridValue(gridPosition, value))
                return false;

            SetValue(gridPosition, value);
            return true;
        }

        private void SetValue(IntXZ gridPos, TValue value)
        {
            gridArray[gridPos.X, gridPos.Z].Value = value;
        }

        public TValue GetValue(int x, int z)
        {
            var gridPos = GetGridPosition(x, z);

            return gridArray[gridPos.X, gridPos.Z].Value;
        }

        public void ClearValue(int x, int z)
        {
            var gridPos = GetGridPosition(x, z);
            gridArray[gridPos.X, gridPos.Z].Value = default;
        }

        public IntXZ GetGridPosition(int x, int z)
        {
            if(!IsInsideGrid(x, z))
                throw new ArgumentOutOfRangeException("Position out of grid");

            return new IntXZ(x / cellSize, z / cellSize);
        }

        public void Fill(TValue value)
        {
            foreach(var gridPos in GridMatrix)
                gridPos.Value = value;
        }

        public virtual Vector3 GetWorldPosition(int x, int y)
        {
            var z = y;
            return new Vector3(x, 0, z) * CellSize;
        }

        public virtual Vector3 GetWorldPosition(int x, int y, TValue value)
        {
            return GetWorldPosition(x, y);
        }

        public virtual bool IsInsideGrid(int x, int z)
        {
            return x >= 0 && x < width
                && z >= 0 && z < height;
        }

        public virtual bool CanSetGridValue(IntXZ gridPosition, TValue value)
        {
            if(!IsInsideGrid(gridPosition.X, gridPosition.Z))
                return false;

            var gridValue = gridArray[gridPosition.X, gridPosition.Z].Value;
            if(
                !ForceSetValue
                && !EqualityComparer<TValue>.Default.Equals(gridValue, default)
            )
                return false;

            return true;
        }

        public virtual bool TrySetGridValue(Vector3 position, TValue value)
        {
            var gridPosition = GetGridPosition((int)position.x, (int)position.z);

            if(!IsInsideGrid(gridPosition.X, gridPosition.Z))
                return false;

            gridArray[gridPosition.X, gridPosition.Z].Value = value;
            return true;
        }

        public virtual bool ClearGridValue(Vector3 position)
        {
            var gridPosition = GetGridPosition((int)position.x, (int)position.z);

            if(!IsInsideGrid(gridPosition.X, gridPosition.Z))
                return false;

            gridArray[gridPosition.X, gridPosition.Z].Value = default;
            return true;
        }

        public virtual bool ClearGridValue(TValue value)
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Depth; y++)
                {
                    if(gridArray[x, y].Value == null)
                        continue;
                    if(!gridArray[x, y].Value.Equals(value))
                        continue;

                    gridArray[x, y].Value = default;
                }
            }


            return true;
        }
    }
}
