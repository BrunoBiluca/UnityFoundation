using NUnit.Framework;
using System;

namespace UnityFoundation.Code.Tests
{
    [TestFixture(typeof(BaseGridTestFixture), typeof(XZ))]
    [TestFixture(typeof(GridXZTestFixture), typeof(XZ))]
    [TestFixture(typeof(GridXYTestFixture), typeof(XY))]
    public class GridTests<TFixture, TPos> where TFixture : IGridTestFixture<TPos, int>, new()
    {
        TFixture fixture;

        [SetUp]
        public void SetUp()
        {
            fixture = new TFixture();
        }

        [Test]
        public void Given_an_empty_grid_should_initialize_with_type_default()
        {
            var grid = fixture.Grid();
            Assert.AreEqual(default(int), grid.GetValue(fixture.Coordinate()));
        }

        [Test]
        public void Should_throw_error_when_get_position_outside_grid()
        {
            var grid = fixture.Grid();
            Assert.Throws<ArgumentOutOfRangeException>(
                () => grid.GetValue(fixture.OutOfGridCoordinate())
            );
        }

        [Test]
        public void Given_position_is_filled_should_not_change_value_when_try_set_it()
        {
            var grid = fixture.Grid();

            var coord = fixture.Coordinate();
            grid.SetValue(coord, 123);

            var coord2 = fixture.SecondCoordinate();
            grid.SetValue(coord2, 456);

            Assert.That(grid.GetValue(coord), Is.EqualTo(123));
            Assert.That(grid.GetValue(coord2), Is.EqualTo(456));
        }

        [Test]
        public void Should_set_value_when_position_was_cleared()
        {
            var grid = fixture.Grid();

            var coord = fixture.Coordinate();
            grid.SetValue(coord, 123);
            grid.SetValue(coord, 456);

            Assert.That(grid.GetValue(coord), Is.EqualTo(123));

            grid.Clear(coord);

            grid.SetValue(coord, 456);
            Assert.That(grid.GetValue(coord), Is.EqualTo(456));
        }
    }
}
