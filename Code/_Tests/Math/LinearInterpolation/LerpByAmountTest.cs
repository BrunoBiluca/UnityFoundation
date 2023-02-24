using NUnit.Framework;
using System.Collections.Generic;

namespace UnityFoundation.Code.Math.Tests
{
    public class LerpByAmountTest
    {

        [Test]
        [TestCaseSource(nameof(TestLerpByValue))]
        public void Should_interpolate_using_value_between_start_and_end_range(
            float startValue, float endValue, float interpolateValue, int expectedSteps
        )
        {
            var lerp = new LerpByValue(startValue, endValue);

            Assert.That(lerp.Eval(0f), Is.EqualTo(startValue).Within(.1f));

            for(var i = 0; i < expectedSteps - 1; i++)
            {
                var value = lerp.Eval(interpolateValue);
                Assert.That(value, Is.Not.EqualTo(endValue).Within(.1f));
            }

            Assert.That(lerp.Eval(interpolateValue), Is.EqualTo(endValue).Within(.1f));
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
