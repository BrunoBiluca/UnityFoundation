using System;
using UnityEngine;
using UnityEngine.UIElements;

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

        public GridXZConfig Config { get; private set; }

        public WorldGridXZ(
            Vector3 initialPosition, int width, int depth, int cellSize
        ) : this(initialPosition, width, depth, cellSize, () => default)
        {
        }

        public WorldGridXZ(
            Vector3 initialPosition, int width, int depth, int cellSize, Func<T> valueFactory
        )
        {
            Config = new GridXZConfig() { Width = width, Depth = depth, CellSize = cellSize };
            InitialPosition = initialPosition;
            CellSize = cellSize;
            WidthPosition = MapGridToWorld(
                new GridCellPositionXZ(width, 0).MapPostitionToScaled(CellSize)
            );
            DepthPosition = MapGridToWorld(
                new GridCellPositionXZ(0, depth).MapPostitionToScaled(CellSize)
            );
            WidthAndDepthPosition = MapGridToWorld(
                new GridCellPositionXZ(width, depth).MapPostitionToScaled(CellSize)
            );

            grid = new GridXZ<T>(width, depth, cellSize, valueFactory);
        }

        public GridCellXZ<T> GetCell(Vector3 worldPosition)
        {
            return grid.GetCell(MapWorldToGrid(worldPosition));
        }

        public Vector3 GetCellWorldPosition(Vector3 worldPosition)
        {
            return MapGridToWorld(MapWorldToGrid(worldPosition));
        }

        public Vector3 GetCellCenterPosition(Vector3 worldPosition)
        {
            return MapGridToWorldCellCenter(MapWorldToGrid(worldPosition));
        }

        public Vector3 GetCellCenterPosition(int x, int z)
        {
            return GetCellCenterPosition(new GridCellPositionXZ(x, z));
        }

        public Vector3 GetCellCenterPosition(GridCellPositionXZ position)
        {
            return MapGridToWorldCellCenter(position.MapPostitionToScaled(CellSize));
        }

        public bool TrySetValue(Vector3 worldPosition, T value)
        {
            return grid.TrySetValue(MapWorldToGrid(worldPosition), value);
        }

        public void ClearValue(Vector3 position)
        {
            grid.ClearValue(MapWorldToGrid(position));
        }

        public T GetValue(Vector3 worldPosition)
        {
            return grid.GetValue(MapWorldToGrid(worldPosition));
        }

        public bool TryUpdateValue(Vector3 worldPosition, Action<T> updateCallback)
        {
            return grid.TryUpdateValue(MapWorldToGrid(worldPosition), updateCallback);
        }

        public void Fill(T value)
        {
            grid.Fill(value);
        }

        // --------------------------------------------------------------------------
        // --------------              PRIVATE METHODS                ---------------
        // --------------------------------------------------------------------------

        /// <summary>
        /// Returns a Scaled Grid Position
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns></returns>
        private GridCellPositionScaledXZ MapWorldToGrid(Vector3 worldPosition)
        {
            return grid.GetCellPosition(
                new GridCellPositionScaledXZ(
                    (int)(worldPosition.x - InitialPosition.x),
                    (int)(worldPosition.z - InitialPosition.z)
                )
            );
        }

        private Vector3 MapGridToWorld(GridCellPositionScaledXZ position)
        {
            return new Vector3(position.X, 0, position.Z) + InitialPosition;
        }

        private Vector3 MapGridToWorldCellCenter(GridCellPositionScaledXZ position)
        {
            return MapGridToWorld(position) + new Vector3(CellSize / 2f, 0f, CellSize / 2f);
        }
    }
}
