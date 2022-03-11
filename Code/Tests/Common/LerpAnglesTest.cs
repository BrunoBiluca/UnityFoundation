using NUnit.Framework;
using System.Collections.Generic;

namespace UnityFoundation.Code.Tests
{
    public class LerpAnglesTest
    {
        [Test]
        [TestCaseSource(nameof(TestLerpByAmount))]
        public void ShouldInterpolateUntilReachEndValue(
            float startValue, float endValue,
            float oneThirdWay, float halfWayValue, float twoThirdWayValue
        )
        {
            var lerp = new Lerp(startValue);
            lerp.SetEndValue(endValue);

            Assert.That(lerp.Eval(0f), Is.EqualTo(startValue).Within(1f));
            Assert.That(lerp.Eval(.33f), Is.EqualTo(oneThirdWay).Within(1f));
            Assert.That(lerp.Eval(.17f), Is.EqualTo(halfWayValue).Within(1f));
            Assert.That(lerp.Eval(.16f), Is.EqualTo(twoThirdWayValue).Within(1f));
            Assert.That(lerp.Eval(.37f), Is.EqualTo(endValue).Within(1f));
        }

        private static IEnumerable<TestCaseData> TestLerpByAmount()
        {
            yield return new TestCaseData(0f, 10f, 3.3f, 5f, 6.6f)
                .SetName("Positive from zero interpolation amount");
            yield return new TestCaseData(2f, 10f, 4f, 6f, 8f)
                .SetName("Arbitrary positive interpolation amount");
            yield return new TestCaseData(-2f, 10f, 2f, 4f, 6f)
                .SetName("From negative to positive interpolation amount");
            yield return new TestCaseData(0f, -10f, -3.3f, -5f, -6.6f)
                .SetName("Negative from zero interpolation amount");
            yield return new TestCaseData(-10f, 10f, -3.3f, 0f, 3.3f)
                .SetName("Negative to positive interpolation amount");
            yield return new TestCaseData(-10f, -2f, -8f, -6f, -4f)
                .SetName("Arbitrary negative interpolation amount");
        }

        [Test]
        [TestCaseSource(nameof(TestLerpByValue))]
        public void ShouldInterpolateByValueUntilReachBaseValue(
            float startValue, float endValue, float interpolateValue, int expectedSteps
        )
        {
            var lerp = new Lerp(startValue);
            lerp.SetEndValue(endValue);

            Assert.That(lerp.EvalBy(0f), Is.EqualTo(startValue).Within(1f));

            for(int i = 0; i < expectedSteps - 1; i++)
            {
                var value = lerp.EvalBy(interpolateValue);
                Assert.That(value, Is.Not.EqualTo(endValue).Within(1f));
            }

            Assert.That(lerp.EvalBy(interpolateValue), Is.EqualTo(endValue).Within(1f));
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

        [Test]
        [TestCaseSource(nameof(TestLerpByAngle))]
        public void ShouldInterpolateByAngle(
            float startValue, float endValue, float interpolateValue, int expectedSteps
        )
        {
            var lerp = new LerpAngle(startValue);
            lerp.SetEndValue(endValue);

            Assert.That(
                lerp.EvalAngle(0f), Is.EqualTo(lerp.StartValue).Within(1f),
                "Fail to validate start value"
            );

            for(int i = 0; i < expectedSteps - 1; i++)
            {
                var value = lerp.EvalAngle(interpolateValue);

                Assert.That(
                    value,
                    Is.Not.EqualTo(lerp.EndValue).Within(.1f),
                    $"Was equal on {i} interaction"
                );
            }

            Assert.That(
                lerp.EvalAngle(interpolateValue), Is.EqualTo(lerp.EndValue).Within(1f),
                "Fail to validate end value"
            );
        }

        private static IEnumerable<TestCaseData> TestLerpByAngle()
        {
            yield return new TestCaseData(270f, 0f, 1f, 90)
                .SetName("From 270 to 0 interpolation by angle");
            yield return new TestCaseData(0f, -90f, 2f, 45)
                .SetName("From zero to negative interpolation by angle");
            yield return new TestCaseData(90f, 0f, 2f, 45)
                .SetName("From positive to zero interpolation by angle");
            yield return new TestCaseData(-90f, 90f, 2f, 90)
                .SetName("From negative to postive interpolation by angle");
            yield return new TestCaseData(-90f, 0f, 2f, 45)
                .SetName("From negative to zero interpolation by angle");
            yield return new TestCaseData(-135f, 135f, 1f, 90)
                .SetName("From left bottom corner to right bottom corner interpolation by angle");
            yield return new TestCaseData(135f, -135f, 1f, 90)
                .SetName("From right bottom corner to left bottom corner interpolation by angle");
            yield return new TestCaseData(-179f, 1f, 1f, 180)
                .SetName("From negative to positive uneven interpolation by angle");
            yield return new TestCaseData(-30f, 10f, 2f, 20)
                .SetName("From negative to positive unevent small range interpolation by angle");
            yield return new TestCaseData(179f, -1f, 1f, 180)
                .SetName("From positive to negative big range interpolation by angle");
            yield return new TestCaseData(10f, -30f, 2f, 20)
                .SetName("From positive to negative small range interpolation by angle");
            yield return new TestCaseData(0f, 720f, 2f, 360)
                .SetName("Two positive cycles interpolation");
            yield return new TestCaseData(-720f, 0f, 2f, 360)
                .SetName("Two negative cycles interpolation");
            yield return new TestCaseData(-720f, 720f, 2f, 720)
                .SetName("Four cycles interpolation");
        }

        [Test]
        public void ShouldInterpolateOriginalAngleWhenCheckMinPathIsDisabled()
        {
            var startValue = -135f;
            var endValue = 135f;
            var interpolateAmount = 1f;
            var expectedSteps = 270;

            var lerp = new LerpAngle(startValue) { CheckMinPath = false };
            lerp.SetEndValue(135f);

            Assert.That(
                lerp.EvalAngle(0f), Is.EqualTo(startValue).Within(1f),
                "Fail to validate start value"
            );

            for(int i = 0; i < expectedSteps - 1; i++)
            {
                var value = lerp.EvalAngle(interpolateAmount);

                Assert.That(
                    value,
                    Is.Not.EqualTo(endValue).Within(.1f),
                    $"Was equal on {i} interaction"
                );
            }

            Assert.That(
                lerp.EvalAngle(interpolateAmount), Is.EqualTo(endValue).Within(1f),
                "Fail to validate end value"
            );
        }

        [Test]
        public void ShouldRetainStateOfInterpolationWhenChangeEndValue()
        {
            var startValue = 0f;
            var middleValue = 10f;
            var endValue = 20f;
            var interpolateAmount = 1f;

            var lerp = new LerpAngle(startValue) { RetainState = true };
            lerp.SetEndValue(middleValue);

            Assert.That(
                lerp.EvalAngle(0f), Is.EqualTo(startValue).Within(1f),
                "Fail to validate start value"
            );

            for(int i = 0; i < 5 - 1; i++)
            {
                var value = lerp.EvalAngle(interpolateAmount);

                Assert.That(
                    value,
                    Is.Not.EqualTo(5f).Within(.1f),
                    $"Was equal on {i} interaction"
                );
            }

            lerp.SetEndValue(20f);

            Assert.That(
                lerp.EvalAngle(interpolateAmount), Is.EqualTo(5f).Within(.1f),
                "Fail to validate middle pathway value"
            );

            for(int i = 0; i < 15 - 1; i++)
            {
                var value = lerp.EvalAngle(interpolateAmount);

                Assert.That(
                    value,
                    Is.Not.EqualTo(endValue).Within(.1f),
                    $"Was equal on {i} interaction"
                );
            }

            Assert.That(
                lerp.EvalAngle(interpolateAmount), Is.EqualTo(endValue).Within(1f),
                "Fail to validate end value"
            );
        }
    }
}