using NUnit.Framework;
using System;

namespace UnityFoundation.Code.Tests
{
    public class GridTests
    {
        [Test]
        public void Given_an_empty_grid_should_initialize_with_type_default()
        {
            var grid = new BaseGrid<GridXZLimits, GridXZCell<int>, XZ, int>(new GridXZLimits(2, 2));

            Assert.AreEqual(default(int), grid.GetValue(new XZ(0, 0)));
            Assert.AreEqual(default(int), grid.GetValue(new XZ(0, 1)));
            Assert.AreEqual(default(int), grid.GetValue(new XZ(1, 0)));
            Assert.AreEqual(default(int), grid.GetValue(new XZ(1, 1)));
        }

        [Test]
        public void Should_throw_error_when_get_position_outside_grid()
        {
            var grid = new BaseGrid<GridXZLimits, GridXZCell<int>, XZ, int>(new GridXZLimits(2, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetValue(new XZ(3, 3)));
        }

        [Test]
        public void Given_position_is_filled_should_not_change_value_when_try_set_it()
        {
            var grid = new BaseGrid<GridXZLimits, GridXZCell<int>, XZ, int>(new GridXZLimits(2, 2));

            var coord = new XZ(0, 0);
            grid.SetValue(coord, 123);

            var coord2 = new XZ(1, 0);
            grid.SetValue(coord2, 456);

            Assert.That(grid.GetValue(coord), Is.EqualTo(123));
            Assert.That(grid.GetValue(coord2), Is.EqualTo(456));
        }

        [Test]
        public void Should_set_value_when_position_was_cleared()
        {
            var grid = new BaseGrid<GridXZLimits, GridXZCell<int>, XZ, int>(new GridXZLimits(2, 2));

            var coord = new XZ(1, 1);
            grid.SetValue(coord, 123);
            grid.SetValue(coord, 456);

            Assert.That(grid.GetValue(coord), Is.EqualTo(123));

            grid.Clear(coord);

            grid.SetValue(coord, 456);
            Assert.That(grid.GetValue(coord), Is.EqualTo(456));
        }

        class TestValue
        {
            public string str;
        }

        [Test]
        public void Given_position_is_filled_should_update_value()
        {
            var grid = new BaseGrid<GridXZLimits, GridXZCell<TestValue>, XZ, TestValue>(
                new GridXZLimits(2, 2)
            );

            var coord = new XZ(0, 0);
            grid.UpdateValue(coord, value => value.str = "updated");

            Assert.That(grid.GetValue(coord).str, Is.EqualTo("updated"));
        }
    }
}
