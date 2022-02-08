using UnityEngine;
using N = NUnit.Framework;

namespace Assets.GameAssets.Player.Tests
{
    public static class AssertHelper
    {
        public static void AreEqual( Vector2 expected, Vector2 actual, float delta = 0f)
        {
            var distance = Vector2.Distance(expected, actual);

            N.Assert.That(0f, N.Is.EqualTo(distance).Within(delta));
        }
    }
}