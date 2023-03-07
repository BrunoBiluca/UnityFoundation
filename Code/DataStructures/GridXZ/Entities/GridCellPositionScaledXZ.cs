using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class GridCellPositionScaledXZ
    {
        public int X { get; private set; }
        public int Z { get; private set; }

        public GridCellPositionScaledXZ(int x, int z)
        {
            X = x; Z = z;
        }
    }
}
