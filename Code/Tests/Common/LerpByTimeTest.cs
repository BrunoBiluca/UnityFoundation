using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityFoundation.Code.Tests
{
    public class LerpByTimeTest
    {
        [Test]
        public void ShouldNotChangeValueIfTimeDidntPassed()
        {
            var lerp = new LerpByTime(0, 10f, 2f);
            Assert.AreEqual(0f, lerp.CurrentValue);
        }

        [Test]
        public void InterpolatePositiveValues()
        {
            var lerp = new LerpByTime(0, 10f, 2f);

            lerp.Eval(1f);
            Assert.AreEqual(5f, lerp.CurrentValue);

            lerp.Eval(1f);
            Assert.AreEqual(10f, lerp.CurrentValue);
        }

        [Test]
        public void InterpolateNegativeValues()
        {
            var lerp = new LerpByTime(-10f, 0f, 2f);

            lerp.Eval(1f);
            Assert.AreEqual(-5f, lerp.CurrentValue);

            lerp.Eval(1f);
            Assert.AreEqual(0f, lerp.CurrentValue);
        }
    }
}