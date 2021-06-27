using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Code
{
    public static class MathX
    {
        public static float Remainder(float value) => Mathf.Floor(value) - value;
    }
}