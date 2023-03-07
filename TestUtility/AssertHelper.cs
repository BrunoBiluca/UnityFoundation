using UnityEngine;
using N = NUnit.Framework;

namespace UnityFoundation.TestUtility
{
    public static class AssertHelper
    {
        public static void AreEqual(Vector2 expected, Vector2 actual, float delta = 0f)
        {
            var distance = Vector2.Distance(expected, actual);

            N.Assert.That(0f, N.Is.EqualTo(distance).Within(delta));
        }

        public static void AreEqual(Vector3 expected, Vector3 actual, float delta = 0f)
        {
            var distance = Vector3.Distance(expected, actual);

            N.Assert.That(distance, N.Is.EqualTo(0f).Within(delta));
        }

        public static void AreNotEqual(Vector2 expected, Vector2 actual, float delta = 0f)
        {
            var distance = Vector2.Distance(expected, actual);

            N.Assert.That(0f, N.Is.Not.EqualTo(distance).Within(delta));
        }

        public static void AreNotEqual(Vector3 expected, Vector3 actual, float delta = 0f)
        {
            var distance = Vector3.Distance(expected, actual);

            N.Assert.That(distance, N.Is.Not.EqualTo(0f).Within(delta));
        }

        public static void MultiEqual(
            object expected, string description, params object[] actuals
        )
        {
            foreach(var actual in actuals)
            {
                N.Assert.AreEqual(expected, actual, description);
            }
        }

        public static void Between(
            float expectedBegin,
            float expectedEnd,
            float actual,
            bool inclusive = true,
            bool inclusiveBegin = true,
            bool inclusiveEnd = true
        )
        {
            if(inclusive)
            {
                N.Assert.GreaterOrEqual(actual, expectedBegin);
                N.Assert.LessOrEqual(actual, expectedEnd);
                return;
            }

            if(inclusiveBegin)
                N.Assert.GreaterOrEqual(actual, expectedBegin);
            else
                N.Assert.Greater(actual, expectedBegin);

            if(inclusiveEnd)
                N.Assert.LessOrEqual(actual, expectedEnd);
            else
                N.Assert.Less(actual, expectedEnd);
        }
    }
}