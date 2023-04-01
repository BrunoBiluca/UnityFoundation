using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class GridXY
    {
        private readonly int width;
        private readonly int height;
        private readonly int cellSize;

        private readonly int[,] gridArray;

        public int Width => width;
        public int Height => height;
        public int CellSize => cellSize;
        public int[,] GridArray => gridArray;

        public GridXY(int width, int height)
        {
            this.width = width;
            this.height = height;

            gridArray = new int[width, height];

            cellSize = 4;
        }

        public Vector2 GetGridPostion(Vector3 position)
        {
            var gridPosition = new Vector2(
                (int)Mathf.Floor(position.x / cellSize),
                (int)Mathf.Floor(position.y / cellSize)
            );

            return gridPosition;
        }

        public bool IsInsideGrid(int x, int y)
        {
            return x >= 0 && x < width
                && y >= 0 && y < height;
        }

        public bool TrySetNodeValue(Vector3 position, int value)
        {
            var gridPosition = GetGridPostion(position);

            if(!IsInsideGrid((int)gridPosition.x, (int)gridPosition.y))
                return false;

            gridArray[(int)gridPosition.x, (int)gridPosition.y] = value;
            return true;
        }
    }
}