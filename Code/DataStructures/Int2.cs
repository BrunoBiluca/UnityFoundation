﻿using UnityEngine;

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
    }
}