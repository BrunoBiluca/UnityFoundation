using NUnit.Framework;
using System;

namespace UnityFoundation.Code.Grid.Tests
{
    public class GridXZTests
    {
        [Test]
        public void Given_an_empty_grid_should_initialize_with_type_default()
        {
            var grid = new GridXZ<int>(2, 2, 1);

            Assert.AreEqual(default(int), grid.GetValue(0, 0));
            Assert.AreEqual(default(int), grid.GetValue(0, 1));
            Assert.AreEqual(default(int), grid.GetValue(1, 0));
            Assert.AreEqual(default(int), grid.GetValue(1, 1));
        }

        [Test]
        public void Should_throw_error_when_get_position_outside_grid()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetCellPosition(4, 4));
        }

        [Test]
        public void Given_position_is_filled_should_not_change_value_when_try_set_it()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.IsFalse(grid.TrySetValue(2, 2, 432));

            Assert.IsTrue(grid.TrySetValue(0, 0, 123));
            Assert.IsFalse(grid.TrySetValue(0, 0, 987));
        }

        [Test]
        public void Given_filled_position_should_change_value_if_force_set_value_is_active()
        {
            var grid = new GridXZ<int>(4, 4, 1) {
                ForceSetValue = true
            };

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.IsTrue(grid.TrySetValue(2, 2, 432));
        }

        [Test]
        public void Should_set_value_when_position_was_cleared()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.IsFalse(grid.TrySetValue(2, 2, 432));

            grid.ClearValue(2, 2);

            Assert.IsTrue(grid.TrySetValue(2, 2, 432));
        }

        [Test]
        public void Given_that_position_exists_inside_should_return_its_value()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.AreEqual(123, grid.GetValue(2, 2));
        }

        [Test]
        public void Should_fill_all_positions_in_grid()
        {
            var grid = new GridXZ<int>(2, 2, 1);

            grid.Fill(123);

            Assert.AreEqual(123, grid.GetValue(0, 0));
            Assert.AreEqual(123, grid.GetValue(0, 1));
            Assert.AreEqual(123, grid.GetValue(1, 0));
            Assert.AreEqual(123, grid.GetValue(1, 1));
        }

        [Test]
        public void Should_return_grid_index_when_requests()
        {
            var grid = new GridXZ<int>(2, 2, 1);

            Assert.AreEqual(0, grid.Cells[0, 0].Position.X);
            Assert.AreEqual(0, grid.Cells[0, 0].Position.Z);

            Assert.AreEqual(0, grid.Cells[0, 1].Position.X);
            Assert.AreEqual(1, grid.Cells[0, 1].Position.Z);

            Assert.AreEqual(1, grid.Cells[1, 0].Position.X);
            Assert.AreEqual(0, grid.Cells[1, 0].Position.Z);

            Assert.AreEqual(1, grid.Cells[1, 1].Position.X);
            Assert.AreEqual(1, grid.Cells[1, 1].Position.Z);
        }

        [Test]
        public void Given_position_filled_should_clear_value()
        {
            var grid = new GridXZ<string>(2, 2, 1);
            grid.TrySetValue(0, 0, "00");
            grid.TrySetValue(0, 1, "01");
            grid.TrySetValue(1, 0, "10");
            grid.TrySetValue(1, 1, "11");

            grid.ClearValue("00");
            grid.ClearValue("10");

            Assert.AreEqual(default(string), grid.GetValue(0, 0));
            Assert.AreEqual("01", grid.GetValue(0, 1));
            Assert.AreEqual(default(string), grid.GetValue(1, 0));
            Assert.AreEqual("11", grid.GetValue(1, 1));
        }

        [Test]
        public void Given_position_is_empty_should_not_update_value()
        {
            var grid = new GridXZ<string>(2, 2, 1);

            var didUpdateCallbackRun = false;
            Assert.IsFalse(grid.TryUpdateValue(
                0, 0,
                (value) => { didUpdateCallbackRun = true; }
            ));
            Assert.IsFalse(didUpdateCallbackRun);
        }

        class TestValue
        {
            public string str;
        }

        [Test]
        public void Given_position_is_filled_should_update_value()
        {
            var grid = new GridXZ<TestValue>(2, 2, 1);
            grid.TrySetValue(0, 0, new TestValue());

            Assert.IsTrue(grid.TryUpdateValue(
                0, 0,
                (value) => { value.str = "updated"; }
            ));
            Assert.AreEqual("updated", grid.GetValue(0, 0).str);
        }
    }
}