using System;

namespace UnityFoundation.Code
{
    [Serializable]
    public class XY
    {
        public int X { get; }
        public int Y { get; }

        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if(obj is not XY xy)
                return false;

            return xy.X == X && xy.Y == Y;
        }

        public override string ToString() => $"({X}, {Y})";

        public override int GetHashCode() => X + Y;
    }
}
