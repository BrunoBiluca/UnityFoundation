using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class GridCellPositionXZ
    {
        public int X { get; private set; }
        public int Z { get; private set; }

        public GridCellPositionXZ(int x, int z)
        {
            X = x; Z = z;
        }
    }
}
