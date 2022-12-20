using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public sealed class GridDebug
    {
        public static void DrawLines(IGridXZBase grid, float duration)
        {
            for(int x = 0; x < grid.Config.Width; x++)
                for(int z = 0; z < grid.Config.Depth; z++)
                    DrawGridCell(grid, x, z, duration);

            DrawGridBorders(grid, duration);
        }

        private static void DrawGridCell(IGridXZBase grid, int x, int z, float duration)
        {
            var cellSize = grid.Config.CellSize;
            var gridCellWorldPos = new Vector3(x * cellSize, 0f, z * cellSize);

            Debug.DrawLine(
                gridCellWorldPos,
                gridCellWorldPos + Vector3.forward * cellSize,
                Color.white,
                duration
            );
            Debug.DrawLine(
                gridCellWorldPos,
                gridCellWorldPos + Vector3.right * cellSize,
                Color.white,
                duration
            );
        }

        private static void DrawGridBorders(IGridXZBase grid, float duration)
        {
            var depth = grid.Config.CellSize * grid.Config.Depth * Vector3.forward;
            var width = grid.Config.CellSize * grid.Config.Width * Vector3.right;
            Debug.DrawLine(
                depth,
                width + depth,
                Color.white,
               duration
            );

            Debug.DrawLine(
                width,
                width + depth,
                Color.white,
                duration
            );
        }

    }
}
