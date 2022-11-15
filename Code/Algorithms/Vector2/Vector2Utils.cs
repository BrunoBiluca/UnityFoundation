using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public static class Vector2Utils
    {
        public static Vector2 Abs(Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }

        public static Vector2 Center(Vector2 vector)
        {
            return new Vector2(vector.x / 2, vector.y / 2);
        }
    }
}