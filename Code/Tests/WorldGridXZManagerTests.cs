using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code.Grid.Tests
{
    public class WorldGridXZManagerTests
    {
        class TestGridValue : IEmptyable
        {
            public string text;

            public void Clear()
            {
                text = null;
            }

            public bool IsEmpty()
            {
                return text == null;
            }
        }

        [Test]
        public void Should_return_all_cells_when_grid_was_only_initialized()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);
            var gridManager = new WorldGridXZManager<string>(grid);

            var cells = gridManager.GetAllAvailableCells().Count();

            Assert.That(cells, Is.EqualTo(4));
        }

        [Test]
        public void Should_return_no_available_cell_when_grid_was_all_filled()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);
            grid.Fill("filled");

            var gridManager = new WorldGridXZManager<string>(grid)
                .ApplyValidator(new EmptyCellGridValidation<string>());

            var cells = gridManager.GetAllAvailableCells().Count();

            Assert.That(cells, Is.EqualTo(0));
        }

        [Test]
        public void Should_return_some_cells_when_values_are_set()
        {
            var grid = new WorldGridXZ<TestGridValue>(Vector3.zero, 2, 2, 1);
            var gridManager = new WorldGridXZManager<TestGridValue>(grid)
                .ApplyValidator(new EmptyCellGridValidation<TestGridValue>());

            grid.TrySetValue(Vector3.zero, new TestGridValue() { text = "zero" });
            grid.TrySetValue(Vector3.one, new TestGridValue() { text = "one" });

            var cells = gridManager.GetAllAvailableCells().Count();

            Assert.That(cells, Is.EqualTo(2));
        }

        [Test]
        public void Should_return_some_cells_when_values_are_updated()
        {
            var grid = new WorldGridXZ<TestGridValue>(Vector3.zero, 2, 2, 1);
            var gridManager = new WorldGridXZManager<TestGridValue>(grid)
                .ApplyValidator(new EmptyCellGridValidation<TestGridValue>());

            grid.TrySetValue(Vector3.zero, new TestGridValue());
            grid.TrySetValue(Vector3.one, new TestGridValue());

            Assert.That(gridManager.GetAllAvailableCells().Count(), Is.EqualTo(4));

            grid.TryUpdateValue(Vector3.zero, (value) => value.text = "zero");
            grid.TryUpdateValue(Vector3.one, (value) => value.text = "one");

            Assert.That(gridManager.GetAllAvailableCells().Count(), Is.EqualTo(2));
        }

        [Test]
        public void Should_return_cell_available_when_cell_is_empty_and_in_range()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);
            var gridManager = new WorldGridXZManager<string>(grid);

            gridManager.ApplyValidator(
                new EmptyCellGridValidation<string>(),
                new RangeGridValidation<string>(grid.Cells[0, 0], 1)
            );

            Assert.That(gridManager.IsCellAvailable(grid.Cells[0, 0]), Is.True);
            Assert.That(gridManager.IsCellAvailable(grid.Cells[0, 1]), Is.True);
            Assert.That(gridManager.IsCellAvailable(grid.Cells[1, 0]), Is.True);
            Assert.That(gridManager.IsCellAvailable(grid.Cells[1, 1]), Is.False);

        }
    }
}