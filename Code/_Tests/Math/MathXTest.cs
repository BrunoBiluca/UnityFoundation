using NUnit.Framework;
using System.Collections.Generic;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Code.Math.Tests
{
    public class MathXTest
    {
        public static IEnumerable<TestCaseData> Distance_given_source()
                => new TestCaseSourceBuilder()
            .Test("a negative and a positive numbers", -10, 10, 20)
            .Test("two positive numbers in ascending order", 10, 30, 20)
            .Test("two positive numbers in descending order", 30, 10, 20)
            .Test("two negative numbers in descending order", -10, -30, 20)
            .Test("two negative numbers in ascending order", -30, -10, 20)
            .Test("two max numbers", float.MaxValue, float.MaxValue, 0)
            .Test("two min numbers", float.MaxValue, float.MaxValue, 0)
            .Test("two oposite max numbers", float.MinValue, float.MaxValue, float.PositiveInfinity)
            .Build();

        public static IEnumerable<TestCaseData> IsBetween_behaviour_given_source()
                => new TestCaseSourceBuilder()
            .Test("Between zero", 0, 0, 0)
            .Test("Between 0 and 1 with min inclusive", 0, 1, 0)
            .Test("Between 0 and 1 with max inclusive", 0, 1, 1)
            .Test("Between negative and positive", -1, 1, 0)
            .Test("Between positive numbers", 2, 10, 5)
            .Test("Between negative numbers", -10, -2, -5)
            .Build();
        public static IEnumerable<TestCaseData> Remap_behaviour_given_source_source()
        => new TestCaseSourceBuilder()
            .Test("positive range", 4f, 0, 10, 0, 100, 40)
            .Test("negative range", -4f, -10, 0, -100, 0, -40)
            .Test("negative range to positive", -5f, -10, 0, 0, 10, 5)
            .Build();

        public static IEnumerable<TestCaseData> ShouldNotBeBetweenTestCaseSource()
        {
            yield return new TestCaseData(1, -1, 0).SetName("Wrong order");
            yield return new TestCaseData(-1, 1, 2).SetName("Value greater than max");
            yield return new TestCaseData(-1, 1, -2).SetName("Value smaller than min");
        }

        [TestCaseSource(nameof(Distance_given_source))]
        public void Distance_given(float a, float b, float expected)
        {
            var distance = MathX.Distance(a, b);
            Assert.AreEqual(expected, distance);
        }

        [Test]
        [TestCaseSource(nameof(IsBetween_behaviour_given_source))]
        public void IsBetween_behaviour_given(float min, float max, float value)
        {
            Assert.IsTrue(MathX.IsBetween(value, min, max));
        }
        [TestCaseSource(nameof(Remap_behaviour_given_source_source))]
        public void Remap_behaviour_given(
            float value,
            float from1,
            float to1,
            float from2,
            float to2,
            float expected
        )
        {
            Assert.That(MathX.Remap(value, from1, to1, from2, to2), Is.EqualTo(expected));
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
        [TestCaseSource(nameof(ShouldNotBeBetweenTestCaseSource))]
        public void ShouldNotBeInBetweenNumbers(float min, float max, float value)
        {
            Assert.IsFalse(MathX.IsBetween(value, min, max));
        }
    }
}