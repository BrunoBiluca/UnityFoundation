using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public sealed class GridDebug
    {
        public static void DrawLines(GridXZConfig config, float duration)
        {
            for(int x = 0; x < config.Width; x++)
                for(int z = 0; z < config.Depth; z++)
                    DrawGridCell(config, x, z, duration);

            DrawGridBorders(config, duration);
        }

        private static void DrawGridCell(GridXZConfig config, int x, int z, float duration)
        {
            var cellSize = config.CellSize;
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

        private static void DrawGridBorders(GridXZConfig config, float duration)
        {
            var depth = config.CellSize * config.Depth * Vector3.forward;
            var width = config.CellSize * config.Width * Vector3.right;
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
