using System;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class WorldGridXZ<T> : GridXZ<T>, IWorldGridXZ<T>
    {
        public Vector3 InitialPosition { get; private set; }

        public Vector3 WidthPosition => MapGridToWorld(new IntXZ(Width, 0));
        public Vector3 DepthPosition => MapGridToWorld(new IntXZ(0, Depth));
        public Vector3 WidthAndDepthPosition => MapGridToWorld(new IntXZ(Width, Depth));

        public WorldGridXZ(
            Vector3 initialPosition, int width, int height, int cellSize
        )
        : base(width, height, cellSize)
        {
            InitialPosition = initialPosition;
        }

        public Vector3 GetCellWorldPosition(Vector3 worldPosition)
        {
            var cellPos = MapWorldToGrid(worldPosition);
            return MapGridToWorld(cellPos);
        }

        public Vector3 GetCellWorldPosition(IntXZ gridPosition)
        {
            return MapGridToWorld(gridPosition);
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
            return new Vector3(cellPos.X, 0, cellPos.Z) * CellSize + InitialPosition;
        }

        private IntXZ MapWorldToGrid(Vector3 worldPosition)
        {
            return GetCellPosition(
                (int)(worldPosition.x - InitialPosition.x),
                (int)(worldPosition.z - InitialPosition.z)
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

        public bool TryUpdatValue(Vector3 worldPosition, Action<T> updateCallback)
        {
            var cellPos = MapWorldToGrid(worldPosition);
            var value = GetValue(cellPos.X, cellPos.Z);

            if(IsValueEmpty(value))
                return false;
                
            updateCallback(value);
            return true;
        }
    }
}
