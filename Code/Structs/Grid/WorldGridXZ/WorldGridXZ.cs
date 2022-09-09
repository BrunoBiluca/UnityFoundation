using System;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class WorldGridXZ<T> : IWorldGridXZ<T>
    {
        private readonly IGridXZ<T> grid;

        public Vector3 InitialPosition { get; private set; }

        public Vector3 WidthPosition { get; private set; }
        public Vector3 DepthPosition { get; private set; }
        public Vector3 WidthAndDepthPosition { get; private set; }

        public int CellSize { get; private set; }
        public int Width => grid.Width;
        public int Depth => grid.Depth;
        public GridCellXZ<T>[,] Cells => grid.Cells;

        public WorldGridXZ(
            Vector3 initialPosition, int width, int depth, int cellSize
        ) : this(initialPosition, width, depth, cellSize, () => default)
        {
        }

        public WorldGridXZ(
            Vector3 initialPosition, int width, int depth, int cellSize, Func<T> valueFactory
        )
        {
            InitialPosition = initialPosition;
            CellSize = cellSize;
            WidthPosition = MapGridToWorld(width, 0) * CellSize;
            DepthPosition = MapGridToWorld(0, depth) * CellSize;
            WidthAndDepthPosition = MapGridToWorld(width, depth) * CellSize;

            grid = new GridXZ<T>(width, depth, cellSize, valueFactory);
        }

        public GridCellXZ<T> GetCell(Vector3 worldPosition)
        {
            var (x, z) = MapWorldToGrid(worldPosition);
            return grid.GetCell(x, z);
        }

        public Vector3 GetCellWorldPosition(Vector3 worldPosition)
        {
            var (x, z) = MapWorldToGrid(worldPosition);
            return MapGridToWorld(x, z);
        }

        public Vector3 GetCellCenterPosition(Vector3 worldPosition)
        {
            var (x, z) = MapWorldToGrid(worldPosition);
            return MapGridToWorldCellCenter(x, z);
        }

        public bool TrySetValue(Vector3 worldPosition, T value)
        {
            var (x, z) = MapWorldToGrid(worldPosition);
            return grid.TrySetValue(x, z, value);
        }

        public void ClearValue(Vector3 position)
        {
            var (x, z) = MapWorldToGrid(position);
            grid.ClearValue(x, z);
        }

        public T GetValue(Vector3 worldPosition)
        {
            var (x, z) = MapWorldToGrid(worldPosition);
            return grid.GetValue(x, z);
        }

        public bool TryUpdateValue(Vector3 worldPosition, Action<T> updateCallback)
        {
            var cellPos = MapWorldToGrid(worldPosition);
            return grid.TryUpdateValue(cellPos.x, cellPos.z, updateCallback);
        }

        public void Fill(T value)
        {
            grid.Fill(value);
        }

        // --------------------------------------------------------------------------
        // --------------              PRIVATE METHODS                ---------------
        // --------------------------------------------------------------------------

        private (int x, int z) MapWorldToGrid(Vector3 worldPosition)
        {
            return grid.GetCellPosition(
                (int)(worldPosition.x - InitialPosition.x),
                (int)(worldPosition.z - InitialPosition.z)
            );
        }

        private Vector3 MapGridToWorld(int x, int z)
        {
            return new Vector3(x, 0, z) + InitialPosition;
        }

        private Vector3 MapGridToWorldCellCenter(int x, int z)
        {
            return MapGridToWorld(x, z) + new Vector3(CellSize / 2f, 0f, CellSize / 2f);
        }
    }
}
