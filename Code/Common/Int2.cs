using UnityEngine;

namespace Assets.UnityFoundation.Code.Common
{
    public class Int2
    {
        private int x;
        private int y;

        public int X => x;
        public int Y => y;

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Int2(Vector2 vector)
        {
            this.x = (int)vector.x;
            this.y = (int)vector.y;
        }
    }
}
