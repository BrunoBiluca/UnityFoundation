using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class IntXZ
    {
        public int X { get; }
        public int Z { get; }

        public IntXZ(int x, int z)
        {
            X = x;
            Z = z;
        }
    }
}
