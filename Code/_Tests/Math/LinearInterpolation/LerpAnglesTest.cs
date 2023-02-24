using NUnit.Framework;
using System.Collections.Generic;

namespace UnityFoundation.Code.Math.Tests
{
    public class LerpAnglesTest
    {
        [Test]
        [TestCaseSource(nameof(TestLerpByAngle))]
        public void Given_start_and_end_angles_should_interpolate_between_at_interpolation_rate(
            float startValue, float endValue, float interpolateValue, int expectedSteps
        )
        {
            var lerp = new LerpAngle(startValue);
            lerp.SetEndValue(endValue);

            Assert.That(
                lerp.EvalAngle(0f), Is.EqualTo(lerp.StartValue).Within(1f),
                "Fail to validate start value"
            );

            for(var i = 0; i < expectedSteps - 1; i++)
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

            for(var i = 0; i < expectedSteps - 1; i++)
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
        public void Should_retain_interpolation_amount_when_change_end_value()
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

            for(var i = 0; i < 5 - 1; i++)
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

            for(var i = 0; i < 15 - 1; i++)
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
        public void Should_interpolate_from_start_to_increased_angle()
        {
            var endAngle = 90f;
            var lerp = new LerpAngle(0f);
            lerp.IncreaseAngle(endAngle);

            var iterations = 10;
            var amount = endAngle / iterations;
            for(var i = 0; i < iterations - 1; i++)
            {
                var currentAngle = lerp.EvalAngle(amount);
                Assert.That(currentAngle, Is.Not.EqualTo(endAngle));
                Assert.That(lerp.IsTargetReached, Is.Not.True);
            }

            var lastAngle = lerp.EvalAngle(amount);
            Assert.That(lastAngle, Is.EqualTo(endAngle));
            Assert.That(lerp.IsTargetReached, Is.True);
        }
    }
}