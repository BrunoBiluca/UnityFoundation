using NUnit.Framework;
using System.Collections.Generic;

namespace UnityFoundation.Code.Math.Tests
{
    public class LerpTest
    {
        [Test]
        [TestCaseSource(nameof(TestLerpByAmount))]
        public void Should_increase_interpolation_until_reach_end_value(
            float startValue, float endValue,
            float oneThirdWay, float halfWayValue, float twoThirdWayValue
        )
        {
            var lerp = new Lerp(startValue);
            lerp.SetEndValue(endValue);

            Assert.That(lerp.Eval(0f), Is.EqualTo(startValue).Within(.1f));
            lerp.ResetInterpolation();

            Assert.That(lerp.Eval(.333f), Is.EqualTo(oneThirdWay).Within(.1f));
            lerp.ResetInterpolation();

            Assert.That(lerp.Eval(.5f), Is.EqualTo(halfWayValue).Within(.1f));
            lerp.ResetInterpolation();

            Assert.That(lerp.Eval(.666f), Is.EqualTo(twoThirdWayValue).Within(.1f));
            lerp.ResetInterpolation();

            Assert.That(lerp.Eval(1f), Is.EqualTo(endValue).Within(.1f));
            lerp.ResetInterpolation();
        }

        private static IEnumerable<TestCaseData> TestLerpByAmount()
        {
            yield return new TestCaseData(0f, 10f, 3.3f, 5f, 6.6f)
                .SetName("Positive from zero interpolation amount");
            yield return new TestCaseData(2f, 10f, 4.6f, 6f, 7.328f)
                .SetName("Arbitrary positive interpolation amount");
            yield return new TestCaseData(-2f, 10f, 2f, 4f, 6f)
                .SetName("From negative to positive interpolation amount");
            yield return new TestCaseData(0f, -10f, -3.3f, -5f, -6.6f)
                .SetName("Negative from zero interpolation amount");
            yield return new TestCaseData(-10f, 10f, -3.3f, 0f, 3.3f)
                .SetName("Negative to positive interpolation amount");
            yield return new TestCaseData(-10f, -2f, -7.336f, -6f, -4.672f)
                .SetName("Arbitrary negative interpolation amount");
        }

        [Test]
        [TestCaseSource(nameof(TestLerpByValue))]
        public void Should_interpolate_by_value(
            float startValue, float endValue, float interpolateValue, int expectedSteps
        )
        {
            var lerp = new Lerp(startValue);
            lerp.SetEndValue(endValue);

            Assert.That(lerp.EvalBy(0f), Is.EqualTo(startValue).Within(.1f));

            for(var i = 0; i < expectedSteps - 1; i++)
            {
                var value = lerp.EvalBy(interpolateValue);
                Assert.That(value, Is.Not.EqualTo(endValue).Within(.1f));
            }

            Assert.That(lerp.EvalBy(interpolateValue), Is.EqualTo(endValue).Within(.1f));
        }

        private static IEnumerable<TestCaseData> TestLerpByValue()
        {
            yield return new TestCaseData(0f, 10f, 2f, 5)
                .SetName("Positive from zero interpolation by value");
            yield return new TestCaseData(2f, 10f, 2f, 4)
                .SetName("Arbitrary positive interpolation by value");
            yield return new TestCaseData(-2f, 10f, 2f, 6)
                .SetName("From negative to positive interpolation by value");
            yield return new TestCaseData(0f, -10f, 2f, 5)
                .SetName("From zero to negative interpolation by value");
            yield return new TestCaseData(-10f, 10f, 2f, 10)
                .SetName("Negative to positive interpolation by value");
            yield return new TestCaseData(-10f, -2f, 2f, 4)
                .SetName("Arbitrary negative interpolation by value");
        }
    }
}
