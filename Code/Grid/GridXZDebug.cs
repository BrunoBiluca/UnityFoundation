using Assets.UnityFoundation.DebugHelper;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Grid
{
    class GridXZDebug<TValue> : IGrid<TValue>
    {
        public readonly GridXZ<TValue> grid;
        private readonly TextMeshPro[,] gridTextArray;

        public int Width => grid.Width;

        public int Height => grid.Height;

        public int CellSize => grid.CellSize;

        public GridPosition<TValue>[,] GridArray => grid.GridArray;

        public GridXZDebug(GridXZ<TValue> grid)
        {
            this.grid = grid;
            gridTextArray = new TextMeshPro[grid.Width, grid.Height];
        }

        public int2 GetGridPostion(Vector3 position)
        {
            return grid.GetGridPostion(position);
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, 0, y) * grid.CellSize;
        }

        public bool IsInsideGrid(int x, int y)
        {
            return grid.IsInsideGrid(x, y);
        }

        public bool TrySetGridValue(Vector3 position, TValue value)
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

        public bool ClearGridValue(Vector3 position)
        {
            var result = grid.ClearGridValue(position);

            Display();
            return result;
        }

        public bool ClearGridValue(TValue value)
        {
            var result = grid.ClearGridValue(value);

            Display();
            return result;
        }

        public void Display()
        {
            for(int x = 0; x < grid.GridArray.GetLength(0); x++)
            {
                for(int z = 0; z < grid.GridArray.GetLength(1); z++)
                {
                    if(gridTextArray[x, z] != null)
                        UnityEngine.Object.Destroy(gridTextArray[x, z]);
                }
            }


            for(int x = 0; x < grid.GridArray.GetLength(0); x++)
            {
                for(int z = 0; z < grid.GridArray.GetLength(1); z++)
                {
                    gridTextArray[x, z] = DebugDraw.DrawWordTextCell(
                        grid.GridArray[x, z].ToString(),
                        GetWorldPosition(x, z),
                        new Vector3(grid.CellSize, 0.5f, grid.CellSize)
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
                GetWorldPosition(0, grid.Height),
                GetWorldPosition(grid.Width, grid.Height),
                Color.white,
                100f
            );
            Debug.DrawLine(
                GetWorldPosition(grid.Width, 0),
                GetWorldPosition(grid.Width, grid.Height),
                Color.white,
                100f
            );
        }
        
        public bool DrawLines(params int2[] gridPositions)
        {
            for(int i = 1; i < gridPositions.Length; i++)
            {
                var centerNodeCorrection = new Vector3(grid.CellSize / 2, grid.CellSize / 2, 0);

                Debug.DrawLine(
                    GetWorldPosition(
                        gridPositions[i - 1].x,
                        gridPositions[i - 1].y
                    ) + centerNodeCorrection,
                    GetWorldPosition(
                        gridPositions[i].x,
                        gridPositions[i].y
                    ) + centerNodeCorrection,
                    Color.white,
                    1000000
                );
            }
            return true;
        }
    }
}
