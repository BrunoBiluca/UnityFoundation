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

        public IntXZ TranslateX(int amount)
        {
            return new IntXZ(X + amount, Z);
        }

        public IntXZ TranslateZ(int amount)
        {
            return new IntXZ(X, Z + amount);
        }
    }
}
