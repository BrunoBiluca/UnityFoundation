using UnityEngine;

namespace UnityFoundation.Code
{
    public class Int2
    {
        private readonly int x;
        private readonly int y;

        public int X => x;
        public int Y => y;

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Int2(Vector2 vector)
        {
            x = (int)vector.x;
            y = (int)vector.y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public override bool Equals(object obj)
        {
            if(obj is Int2 other)
                return other.x == x && other.y == y;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
