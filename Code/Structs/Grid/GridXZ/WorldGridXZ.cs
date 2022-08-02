using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class WorldGridXZ<T> : GridXZ<T>, IWorldGridXZ<T>
    {
        private readonly Vector3 initialPosition;

        public WorldGridXZ(
            Vector3 initialPosition, int width, int height, int cellSize
        )
        : base(width, height, cellSize)
        {
            this.initialPosition = initialPosition;
        }

        public Vector3 GetCellWorldPosition(Vector3 worldPosition)
        {
            var cellPos = MapWorldToGrid(worldPosition);
            return MapGridToWorld(cellPos);
        }

        public Vector3 GetCellCenterPosition(Vector3 worldPosition)
        {
            var cellPos = MapWorldToGrid(worldPosition);

            var cellWorldPosition = MapGridToWorld(cellPos);
            return new Vector3(
                cellWorldPosition.x + CellSize / 2f,
                0f,
                cellWorldPosition.z + CellSize / 2f
            );
        }

        private Vector3 MapGridToWorld(IntXZ cellPos)
        {
            return new Vector3(cellPos.X, 0, cellPos.Z) * CellSize + initialPosition;
        }

        private IntXZ MapWorldToGrid(Vector3 worldPosition)
        {
            return GetCellPosition(
                (int)(worldPosition.x - initialPosition.x),
                (int)(worldPosition.z - initialPosition.z)
            );
        }

        public bool TrySetValue(Vector3 worldPosition, T value)
        {
            var cellPos = MapWorldToGrid(worldPosition);
            return TrySetValue(cellPos.X, cellPos.Z, value);
        }

        public void ClearValue(Vector3 position)
        {
            var cell = MapWorldToGrid(position);

            SetValueDefault(cell);
        }

        public T GetValue(Vector3 worldPosition)
        {
            var cellPos = MapWorldToGrid(worldPosition);
            return GetValue(cellPos.X, cellPos.Z);
        }
    }
}
