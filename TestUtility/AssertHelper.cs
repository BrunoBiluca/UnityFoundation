using UnityEngine;
using N = NUnit.Framework;

namespace Assets.GameAssets.Player.Tests
{
    public static class AssertHelper
    {
        public static void AreEqual(Vector2 expected, Vector2 actual, float delta = 0f)
        {
            var distance = Vector2.Distance(expected, actual);

            N.Assert.That(0f, N.Is.EqualTo(distance).Within(delta));
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
            if(inclusive){
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