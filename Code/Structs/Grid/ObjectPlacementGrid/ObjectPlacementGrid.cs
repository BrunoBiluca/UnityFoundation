using UnityEngine;

namespace UnityFoundation.Code.Grid.ObjectPlacementGrid
{
    public class ObjectPlacementGrid : GridXZ<GridObject>
    {
        public ObjectPlacementGrid(int width, int height, int cellSize)
            : base(width, height, cellSize)
        {
        }

        public bool CanSetGridValue(int x, int z, GridObject value)
        {
            var objDimentions = GridObjectDimentions(new Int2(x, z), value);

            for(int objX = x; x < objDimentions.X; x++)
                for(int objZ = z; objZ < objDimentions.Y; objZ++)
                    if(!CanSetGridValue(objX, objZ))
                        return false;

            return true;
        }

        public bool TrySetGridValue(Vector3 position, GridObject value)
        {
            var gridPosition = new Int2((int)position.x, (int)position.z);

            if(!CanSetGridValue(gridPosition.X, gridPosition.Y, value))
                return false;

            var objDimentions = GridObjectDimentions(gridPosition, value);
            for(int x = gridPosition.X; x < objDimentions.X; x++)
                for(int y = gridPosition.Y; y < objDimentions.Y; y++)
                    TrySetValue(x, y, value);

            return true;
        }

        public Vector3 GetWorldPosition(int x, int y, GridObject gridObject)
        {
            return new Vector3(x, 0f, y) + CalculatePositionOffset(gridObject);
        }

        private Vector3 CalculatePositionOffset(GridObject gridObject)
        {
            var offsetX = gridObject.Width;
            var offsetY = gridObject.Height;

            if(
                gridObject.Direction == GridObjectDirection.LEFT
                || gridObject.Direction == GridObjectDirection.RIGHT
            )
            {
                offsetX = gridObject.Height;
                offsetY = gridObject.Width;
            }

            return new Vector3(
               offsetX * gridObject.Direction.Offset.X * CellSize,
               0f,
               offsetY * gridObject.Direction.Offset.Y * CellSize
            );
        }

        private static Int2 GridObjectDimentions(Int2 gridPosition, GridObject value)
        {
            var objectDimensionX = gridPosition.X + value.Width;
            var objectDimensionY = gridPosition.Y + value.Height;

            if(value.Direction == GridObjectDirection.LEFT
                || value.Direction == GridObjectDirection.RIGHT)
            {
                objectDimensionX = gridPosition.X + value.Height;
                objectDimensionY = gridPosition.Y + value.Width;
            }

            return new Int2(objectDimensionX, objectDimensionY);
        }

    }
}