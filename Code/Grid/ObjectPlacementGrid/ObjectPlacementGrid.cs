using Unity.Mathematics;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Grid.ObjectPlacementGrid
{
    public class ObjectPlacementGrid : GridXZ<GridObject>
    {
        public ObjectPlacementGrid(int width, int height, int cellSize)
            : base(width, height, cellSize)
        {
        }

        public override bool CanSetGridValue(int2 gridPosition, GridObject value)
        {
            var objDimentions = GridObjectDimentions(gridPosition, value);

            for(int x = gridPosition.x; x < objDimentions.x; x++)
                for(int y = gridPosition.y; y < objDimentions.y; y++)
                    if(!base.CanSetGridValue(new int2(x, y), value))
                        return false;

            return true;
        }

        public override bool TrySetGridValue(Vector3 position, GridObject value)
        {
            var gridPosition = GetGridPostion(position);

            if(!CanSetGridValue(gridPosition, value))
                return false;

            var objDimentions = GridObjectDimentions(gridPosition, value);
            for(int x = gridPosition.x; x < objDimentions.x; x++)
                for(int y = gridPosition.y; y < objDimentions.y; y++)
                    gridArray[x, y].Value = value;

            return true;
        }

        public override Vector3 GetWorldPosition(int x, int y, GridObject gridObject)
        {
            return GetWorldPosition(x, y) + CalculatePositionOffset(gridObject);
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
               offsetX * gridObject.Direction.Offset.x * CellSize,
               0f,
               offsetY * gridObject.Direction.Offset.y * CellSize
            );
        }

        private static int2 GridObjectDimentions(int2 gridPosition, GridObject value)
        {
            var objectDimensionX = gridPosition.x + value.Width;
            var objectDimensionY = gridPosition.y + value.Height;

            if(value.Direction == GridObjectDirection.LEFT
                || value.Direction == GridObjectDirection.RIGHT)
            {
                objectDimensionX = gridPosition.x + value.Height;
                objectDimensionY = gridPosition.y + value.Width;
            }

            return new int2(objectDimensionX, objectDimensionY);
        }

    }
}