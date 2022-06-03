using System;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class GridXZ<TValue> : IGrid<TValue>
    {
        protected readonly int width;
        protected readonly int height;
        protected readonly int cellSize;
        protected readonly GridPosition<TValue>[,] gridArray;

        public int Width => width;
        public int Height => height;
        public int CellSize => cellSize;
        public GridPosition<TValue>[,] GridArray => gridArray;

        public GridXZ(
            int width,
            int height,
            int cellSize
        )
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridArray = new GridPosition<TValue>[width, height];
            for(int x = 0; x < width; x++)
                for(int z = 0; z < height; z++)
                    gridArray[x, z] = new GridPosition<TValue>(x, z);
        }

        public virtual Int2 GetGridPostion(Vector3 position)
        {
            return new Int2(
                (int)Math.Floor(position.x / cellSize),
                (int)Math.Floor(position.z / cellSize)
            );
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

        public virtual bool IsInsideGrid(int x, int y)
        {
            return x >= 0 && x < width
                && y >= 0 && y < height;
        }

        public virtual bool CanSetGridValue(Int2 gridPosition, TValue value)
        {
            if(!IsInsideGrid(gridPosition.X, gridPosition.Y))
                return false;

            if(gridArray[gridPosition.Y, gridPosition.Y].Value != null)
                return false;

            return true;
        }

        public virtual bool TrySetGridValue(Vector3 position, TValue value)
        {
            var gridPosition = GetGridPostion(position);

            if(!IsInsideGrid(gridPosition.X, gridPosition.Y))
                return false;

            gridArray[gridPosition.X, gridPosition.Y].Value = value;
            return true;
        }

        public virtual bool TrySetGridValue(int x, int y, TValue value)
        {
            if(!IsInsideGrid(x, y))
                return false;

            gridArray[x, y].Value = value;
            return true;
        }

        public virtual bool ClearGridValue(Vector3 position)
        {
            var gridPosition = GetGridPostion(position);

            if(!IsInsideGrid(gridPosition.X, gridPosition.Y))
                return false;

            gridArray[gridPosition.X, gridPosition.Y].Value = default;
            return true;
        }

        public virtual bool ClearGridValue(TValue value)
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
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
