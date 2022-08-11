using System;

namespace UnityFoundation.Code.Grid
{
    public partial class GridXZ<TValue> : IGridXZ<TValue>
    {
        /// <summary>
        /// Grid position that exists inside grid
        /// Can be used only on GridXZ scope
        /// </summary>
        [Serializable]
        private class GridPositionXZ
        {
            public int X { get; }
            public int Z { get; }

            public GridPositionXZ(int x, int z)
            {
                X = x;
                Z = z;
            }

            public GridPositionXZ TranslateX(int amount)
            {
                return new GridPositionXZ(X + amount, Z);
            }

            public GridPositionXZ TranslateZ(int amount)
            {
                return new GridPositionXZ(X, Z + amount);
            }

            public override string ToString()
            {
                return $"(x: {X}, z: {Z})";
            }
        }
    }

}
