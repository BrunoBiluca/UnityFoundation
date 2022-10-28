using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public static class GridCellPositionXZMapper
    {
        public static GridCellPositionXZ MapScaledToPosition(
            this GridCellPositionScaledXZ scaledPosition, int cellSize
        )
        {
            return new GridCellPositionXZ(scaledPosition.X / cellSize, scaledPosition.Z / cellSize);
        }

        public static GridCellPositionScaledXZ MapPostitionToScaled(
            this GridCellPositionXZ scaledPosition, int cellSize
        )
        {
            return new GridCellPositionScaledXZ(
                scaledPosition.X * cellSize,
                scaledPosition.Z * cellSize
            );
        }
    }
}
