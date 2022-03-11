using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public class MathXTest
    {

        [Test]
        [TestCase(-10, 10, 20)]
        [TestCase(10, 30, 20)]
        [TestCase(30, 10, 20)]
        [TestCase(-10, -30, 20)]
        [TestCase(-30, -10, 20)]
        public void ShouldSumRangesWhenDistanceBetweenNumbers(
            float a, float b, float expected
        )
        {
            var distance = MathX.Distance(a, b);
            Assert.AreEqual(expected, distance);
        }
    }
}