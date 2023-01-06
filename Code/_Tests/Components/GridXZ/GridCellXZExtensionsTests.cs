using NUnit.Framework;

namespace UnityFoundation.Code.Grid.Tests
{
    public class GridCellXZExtensionsTests
    {
        [Test]
        public void Should_be_in_range_if_grid_cell_is_the_same()
        {
            var gridCell = new GridCellXZ<int>(0, 0);

            Assert.That(gridCell.IsInRange(gridCell, 0), Is.True);
        }

        [Test]
        public void Should_not_be_in_range_when_cells_are_different()
        {
            var gridCell = new GridCellXZ<int>(0, 0);
            var gridCell2 = new GridCellXZ<int>(0, 1);

            Assert.That(gridCell.IsInRange(gridCell2, 0), Is.False);
        }

        [Test]
        public void Should_be_in_range_when_cells_are_different_but_range_is_valid()
        {
            var gridCell = new GridCellXZ<int>(1, 2);
            var gridCell2 = new GridCellXZ<int>(3, 3);

            Assert.That(gridCell.IsInRange(gridCell2, 3), Is.True);
        }
    }
}