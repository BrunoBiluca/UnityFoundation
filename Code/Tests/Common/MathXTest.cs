using NUnit.Framework;
using System.Collections.Generic;

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

        [Test]
        [TestCaseSource(nameof(ShouldBeInBetweenTestCaseSource))]
        public void ShouldBeInBetweenNumbers(float min, float max, float value)
        {
            Assert.IsTrue(MathX.IsBetween(value, min, max));
        }

        public static IEnumerable<TestCaseData> ShouldBeInBetweenTestCaseSource()
        {
            yield return new TestCaseData(0, 0, 0).SetName("Between zero");
            yield return new TestCaseData(0, 1, 0).SetName("Between 0 and 1 with min inclusive");
            yield return new TestCaseData(0, 1, 1).SetName("Between 0 and 1 with max inclusive");
            yield return new TestCaseData(-1, 1, 0).SetName("Between negative and positive");
            yield return new TestCaseData(2, 10, 5).SetName("Between positive numbers");
            yield return new TestCaseData(-10, -2, -5).SetName("Between negative numbers");
        }

        [Test]
        [TestCaseSource(nameof(ShouldNotBeBetweenTestCaseSource))]
        public void ShouldNotBeInBetweenNumbers(float min, float max, float value)
        {
            Assert.IsFalse(MathX.IsBetween(value, min, max));
        }

        public static IEnumerable<TestCaseData> ShouldNotBeBetweenTestCaseSource()
        {
            yield return new TestCaseData(1, -1, 0).SetName("Wrong order");
            yield return new TestCaseData(-1, 1, 2).SetName("Value greater than max");
            yield return new TestCaseData(-1, 1, -2).SetName("Value smaller than min");
        }

        [Test]
        public void ShouldBeInBetweenNumberWithoutOrder()
        {
            Assert.IsTrue(MathX.IsBetweenWithoutOrder(0, 1, -1));
        }

        [Test]
        public void ShouldClampWithoutOrder()
        {
            var numPositive = MathX.ClampWithoutOrder(6f, 2f, -2f);
            Assert.AreEqual(numPositive, 2f);

            var numNegative = MathX.ClampWithoutOrder(-6f, 2f, -2f);
            Assert.AreEqual(numNegative, -2f);
        }
    }
}