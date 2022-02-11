using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.UnityFoundation.Code.Common
{
    public static class UIntMathExtensions
    {

        public static uint Clamp(this uint value, uint min, uint max)
        {
            if(value < min)
                return min;

            if(value > max)
                return max;

            return value;
        }

    }
}
