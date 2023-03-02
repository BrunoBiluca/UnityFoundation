using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code.Math.Tests
{
    public class Vector3ExtensionsTest
    {
        [Test]
        public void Should_return_positions_with_range()
        {
            var positions = Vector3.one.PositionsInRange(1, 1).ToList();
            Assert.That(positions.Count, Is.EqualTo(5));

            var positions2 = Vector3.one.PositionsInRange(2, 1).ToList();
            Assert.That(positions2.Count, Is.EqualTo(13));
        }
    }
}
