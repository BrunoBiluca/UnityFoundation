using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public class GridXYLimitsTests
    {
        [Test]
        public void Index_evaluation_behaviour_given_coordinate()
        {
            var limits = new GridLimitXY(1, 3);

            Assert.That(limits.GetIndex(new(0, 0)), Is.EqualTo(0));
            Assert.That(limits.GetIndex(new(0, 1)), Is.EqualTo(1));
            Assert.That(limits.GetIndex(new(0, 2)), Is.EqualTo(2));
        }
    }
}
