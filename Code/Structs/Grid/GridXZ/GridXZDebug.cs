using System;
using TMPro;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Code.Grid
{
    public class GridXZDebug<TValue> : GridXZ<TValue>
    {
        private Transform parent;
        public readonly GridXZ<TValue> grid;
        private readonly TextMeshPro[,] gridTextArray;

        public GridXZDebug(GridXZ<TValue> grid)
            : base(grid.Width, grid.Depth, grid.CellSize)
        {
            this.grid = grid;
            gridTextArray = new TextMeshPro[grid.Width, grid.Depth];

            Display();
        }

        public override bool CanSetGridValue(IntXZ gridPosition, TValue value)
        {
            return grid.CanSetGridValue(gridPosition, value);
        }

        public Int2 GetGridPostion(Vector3 position)
        {
            var pos = grid.GetGridPosition((int)position.x, (int)position.z);
            return new Int2(pos.X, pos.Z);
        }

        public override Vector3 GetWorldPosition(int x, int y)
        {
            return grid.GetWorldPosition(x, y);
        }

        public override Vector3 GetWorldPosition(int x, int y, TValue value)
        {
            return grid.GetWorldPosition(x, y, value);
        }

        public override bool IsInsideGrid(int x, int y)
        {
            return grid.IsInsideGrid(x, y);
        }

        public override bool TrySetGridValue(Vector3 position, TValue value)
        {
            try
            {
                if(!grid.TrySetGridValue(position, value))
                    return false;
            }
            catch(FormatException)
            {
                // Don't change grid value
            }

            Display();
            return true;
        }

        public override bool ClearGridValue(Vector3 position)
        {
            var result = grid.ClearGridValue(position);

            Display();
            return result;
        }

        public override bool ClearGridValue(TValue value)
        {
            var result = grid.ClearGridValue(value);

            Display();
            return result;
        }

        public void Display()
        {
            if(parent == null)
                parent = new GameObject("debug_grid_xz").transform;

            TransformUtils.RemoveChildObjects(parent);

            for(int x = 0; x < grid.GridMatrix.GetLength(0); x++)
            {
                for(int z = 0; z < grid.GridMatrix.GetLength(1); z++)
                {
                    gridTextArray[x, z] = DebugDraw.DrawWordTextCell(
                        grid.GridMatrix[x, z].ToString(),
                        GetWorldPosition(x, z),
                        new Vector3(grid.CellSize, 0.5f, grid.CellSize),
                        parent
                    );
                    Debug.DrawLine(
                        GetWorldPosition(x, z),
                        GetWorldPosition(x, z + 1),
                        Color.white,
                        100f
                    );
                    Debug.DrawLine(
                        GetWorldPosition(x, z),
                        GetWorldPosition(x + 1, z),
                        Color.white,
                        100f
                    );
                }
            }
            Debug.DrawLine(
                GetWorldPosition(0, grid.Depth),
                GetWorldPosition(grid.Width, grid.Depth),
                Color.white,
                100f
            );
            Debug.DrawLine(
                GetWorldPosition(grid.Width, 0),
                GetWorldPosition(grid.Width, grid.Depth),
                Color.white,
                100f
            );
        }
    }
}
