using System;
using TMPro;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Code.Grid
{
    public class GridXYDebug
    {
        private readonly GridXY grid;
        private readonly TextMeshPro[,] gridTextArray;

        public GridXYDebug(GridXY grid)
        {
            this.grid = grid;
            gridTextArray = new TextMeshPro[grid.Width, grid.Height];
        }

        public void Display()
        {
            for(int x = 0; x < grid.GridArray.GetLength(0); x++)
            {
                for(int y = 0; y < grid.GridArray.GetLength(1); y++)
                {
                    gridTextArray[x, y] = DebugDraw.DrawWordTextCell(
                        grid.GridArray[x, y].ToString(),
                        GetWorldPosition(x, y),
                        new Vector3(grid.CellSize, grid.CellSize)
                    );
                    Debug.DrawLine(
                        GetWorldPosition(x, y),
                        GetWorldPosition(x, y + 1),
                        Color.white,
                        100f
                    );
                    Debug.DrawLine(
                        GetWorldPosition(x, y),
                        GetWorldPosition(x + 1, y),
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

        public Vector2 GetGridPostion(Vector3 position)
        {
            return grid.GetGridPostion(position);
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * grid.CellSize;
        }

        public bool TrySetNodeValue(Vector3 position, string value)
        {
            try
            {
                if(!grid.TrySetNodeValue(position, int.Parse(value)))
                    return false;
            }
            catch(FormatException)
            {
                // Don't change grid value
            }

            var gridPosition = grid.GetGridPostion(position);
            gridTextArray[(int)gridPosition.x, (int)gridPosition.y].text = value;
            return true;
        }

        public bool DrawLines(params Int2[] gridPositions)
        {
            for(int i = 1; i < gridPositions.Length; i++)
            {
                var centerNodeCorrection = new Vector3(grid.CellSize / 2, grid.CellSize / 2, 0);

                Debug.DrawLine(
                    GetWorldPosition(
                        gridPositions[i - 1].X,
                        gridPositions[i - 1].Y
                    ) + centerNodeCorrection,
                    GetWorldPosition(
                        gridPositions[i].X,
                        gridPositions[i].Y
                    ) + centerNodeCorrection,
                    Color.white,
                    1000000
                );
            }
            return true;
        }
    }
}