using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class GridXZConfig
    {
        public int Width;
        public int Depth;
        public int CellSize;

        [Serializable]
        public class Position
        {
            public int X;
            public int Z;
        }

    }
}
