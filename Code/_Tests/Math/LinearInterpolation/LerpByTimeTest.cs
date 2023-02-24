using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityFoundation.Code.Math.Tests
{
    public class LerpByTimeTest
    {
        [Test]
        public void Should_not_change_value_if_time_is_not_evaluated()
        {
            var lerp = new LerpByTime(0, 10f, 2f);
            Assert.AreEqual(0f, lerp.CurrentValue);
        }

        [Test]
        public void Should_interpolate_by_time_with_positive_values()
        {
            var lerp = new LerpByTime(0, 10f, 2f);

            lerp.Eval(1f);
            Assert.AreEqual(5f, lerp.CurrentValue);

            lerp.Eval(1f);
            Assert.AreEqual(10f, lerp.CurrentValue);
        }

        [Test]
        public void Should_interpolate_by_time_with_negative_values()
        {
            var lerp = new LerpByTime(-10f, 0f, 2f);

            lerp.Eval(1f);
            Assert.AreEqual(-5f, lerp.CurrentValue);

            lerp.Eval(1f);
            Assert.AreEqual(0f, lerp.CurrentValue);
        }
    }
}