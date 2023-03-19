using NUnit.Framework;

namespace UnityFoundation.Code.Tests
{
    public class BaseGridTests
    {
        class TestValue
        {
            public string str;
        }

        [Test]
        public void Given_position_is_filled_should_update_value()
        {
            var grid = new BaseGrid<GridXZLimits, GridCell<TestValue>, XZ, TestValue>(
                new GridXZLimits(2, 2)
            );

            var coord = new XZ(0, 0);
            grid.UpdateValue(coord, value => value.str = "updated");

            Assert.That(grid.GetValue(coord).str, Is.EqualTo("updated"));
        }
    }
}
