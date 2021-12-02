using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Code
{
    public class Vector3Utils
    {
        public static bool NearlyEqual(Vector3 a, Vector3 b, float delta)
        {
            return MathX.NearlyEqual(a.x, b.x, delta)
            && MathX.NearlyEqual(a.y, b.y, delta)
            && MathX.NearlyEqual(a.z, b.z, delta);
        }
    }
}