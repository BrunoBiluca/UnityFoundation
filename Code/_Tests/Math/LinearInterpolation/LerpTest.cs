using NUnit.Framework;
using System.Collections.Generic;

namespace UnityFoundation.Code.Math.Tests
{
    public class LerpTest
    {
        [Test]
        [TestCaseSource(nameof(TestLerpByPercentage))]
        public void Should_interpolate_using_percentage_values_until_reach_end_value(
            float startValue, float endValue,
            float oneThirdWay, float halfWayValue, float twoThirdWayValue
        )
        {
            var lerp = new Lerp(startValue, endValue);

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

        private static IEnumerable<TestCaseData> TestLerpByPercentage()
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
    }
}
