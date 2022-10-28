using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Code.Grid.Tests
{
    public class WorldGridXZTests
    {
        public static IEnumerable<TestCaseData> MultipleInitialPositions()
        {
            yield return new TestCaseData(Vector3.zero, 1).SetName("Zero");
            yield return new TestCaseData(Vector3.one, 1).SetName("One");
            yield return new TestCaseData(-Vector3.one, 1).SetName("Minus One");
            yield return new TestCaseData(Vector3.zero, 2).SetName("Zero with cell size 2");
        }

        [Test]
        [TestCaseSource(nameof(MultipleInitialPositions))]
        public void Given_filled_grid_should_return_value_if_world_position_inside_grid(
            Vector3 initialPosition, int cellSize
        )
        {
            var grid = new WorldGridXZ<string>(initialPosition, 2, 2, cellSize);
            var bottomLeftPositions = GetWorldPositions(initialPosition, cellSize, Vector3.zero);

            foreach(var pos in bottomLeftPositions)
            {
                grid.TrySetValue(pos, pos.ToString());
                Assert.AreEqual(pos, grid.GetCellWorldPosition(pos), "cell bottom left points");
                Assert.AreEqual(pos.ToString(), grid.GetValue(pos));
            }

            var topRightPositions = GetWorldPositions(
                initialPosition,
                cellSize,
                new Vector3(cellSize - 0.1f, 0, cellSize - 0.1f)
            );

            var i = 0;
            foreach(var pos in topRightPositions)
            {
                var relativePos = bottomLeftPositions[i];
                var cellPos = grid.GetCellWorldPosition(pos);
                Assert.AreEqual(relativePos, cellPos, "cell top right points");
                Assert.AreEqual(relativePos.ToString(), grid.GetValue(pos));
                i++;
            }
        }

        private List<Vector3> GetWorldPositions(
            Vector3 initialPosition,
            int cellSize,
            Vector3 threshold
        )
        {
            var positions = new List<Vector3> {
                // cell bottom left points
                new Vector3(0, 0, 0) + initialPosition + threshold,
                new Vector3(0, 0, cellSize) + initialPosition + threshold,
                new Vector3(cellSize, 0, 0) + initialPosition + threshold,
                new Vector3(cellSize, 0, cellSize) + initialPosition + threshold
            };

            return positions;
        }

        [Test]
        public void Should_return_center_of_cell_world_position()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);

            Assert.AreEqual(
                new Vector3(0.5f, 0, 0.5f), grid.GetCellCenterPosition(new Vector3(0, 0, 0))
            );
            Assert.AreEqual(
                new Vector3(0.5f, 0, 1.5f), grid.GetCellCenterPosition(new Vector3(0, 0, 1))
            );
            Assert.AreEqual(
                new Vector3(1.5f, 0, 0.5f), grid.GetCellCenterPosition(new Vector3(1, 0, 0))
            );
            Assert.AreEqual(
                new Vector3(1.5f, 0, 1.5f), grid.GetCellCenterPosition(new Vector3(1, 0, 1))
            );
        }

        [Test]
        public void Should_return_center_of_cell_world_position_for_custom_cell_size()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 2);

            AssertHelper.MultiEqual(
                new Vector3(1, 0, 1),
                "World position x 1 z 1",
                grid.GetCellCenterPosition(new Vector3(0, 0, 0)),
                grid.GetCellCenterPosition(new Vector3(1, 0, 0)),
                grid.GetCellCenterPosition(new Vector3(0, 0, 1)),
                grid.GetCellCenterPosition(new Vector3(1, 0, 1))
            );

            AssertHelper.MultiEqual(
                new Vector3(3, 0, 1),
                "World position x 3 z 1",
                grid.GetCellCenterPosition(new Vector3(2, 0, 0)),
                grid.GetCellCenterPosition(new Vector3(2, 0, 1)),
                grid.GetCellCenterPosition(new Vector3(3, 0, 0)),
                grid.GetCellCenterPosition(new Vector3(3, 0, 1))
            );

            Assert.AreEqual(
                new Vector3(3, 0, 1),
                grid.GetCellCenterPosition(new Vector3(2, 0, 0))
            );

            Assert.AreEqual(
                new Vector3(3, 0, 3),
                grid.GetCellCenterPosition(new Vector3(2, 0, 2))
            );
        }

        [Test]
        public void Given_position_filled_should_clear_value()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);
            grid.Fill("filled");

            grid.ClearValue(new Vector3(0, 0, 0));
            grid.ClearValue(new Vector3(1, 0, 1));

            Assert.AreEqual(default(string), grid.GetValue(new Vector3(0, 0, 0)));
            Assert.AreEqual("filled", grid.GetValue(new Vector3(0, 0, 1)));
            Assert.AreEqual("filled", grid.GetValue(new Vector3(1, 0, 0)));
            Assert.AreEqual(default(string), grid.GetValue(new Vector3(1, 0, 1)));
        }

        [Test]
        public void Given_position_is_empty_should_set_value()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);

            var cellWorldPosition = new Vector3(0, 0, 1);
            var result = grid.TrySetValue(cellWorldPosition, "filled");

            Assert.IsTrue(result);
            Assert.AreEqual("filled", grid.GetValue(cellWorldPosition));
        }

        [Test]
        public void Given_position_filled_should_not_set_value()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);
            grid.Fill("filled_before");

            var cellWorldPosition = new Vector3(0, 0, 1);
            var result = grid.TrySetValue(cellWorldPosition, "filled");

            Assert.IsFalse(result);
            Assert.AreEqual("filled_before", grid.GetValue(cellWorldPosition));
        }

        [Test]
        public void Given_position_is_empty_should_not_update_value()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);

            Assert.AreEqual(default(string), grid.GetValue(Vector3.zero));

            var isValueUpdated = grid.TryUpdateValue(
                Vector3.zero,
                (value) => value = "updated " + value.Length
            );

            Assert.IsFalse(isValueUpdated);
            Assert.AreEqual(default(string), grid.GetValue(Vector3.zero));
        }

        [Test]
        public void Given_position_is_filled_should_update_value()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 2, 2, 1);

            var isValueSet = grid.TrySetValue(Vector3.zero, "filled");

            Assert.IsTrue(isValueSet);
            Assert.AreEqual("filled", grid.GetValue(Vector3.zero));

            var isValueUpdated = grid.TryUpdateValue(
                Vector3.zero,
                (value) => value = "updated"
            );

            Assert.IsTrue(isValueUpdated);
            Assert.AreEqual("filled", grid.GetValue(Vector3.zero));
        }

        [Test]
        public void Given_grid_cell_position_should_return_world_grid_cell_center_position()
        {
            var grid = new WorldGridXZ<string>(Vector3.zero, 3, 3, 2);

            Assert.That(grid.GetCellCenterPosition(0, 0), Is.EqualTo(new Vector3(1, 0, 1)));
            Assert.That(grid.GetCellCenterPosition(0, 1), Is.EqualTo(new Vector3(1, 0, 3)));
            Assert.That(grid.GetCellCenterPosition(0, 2), Is.EqualTo(new Vector3(1, 0, 5)));
            
            Assert.That(grid.GetCellCenterPosition(1, 0), Is.EqualTo(new Vector3(3, 0, 1)));
            Assert.That(grid.GetCellCenterPosition(1, 1), Is.EqualTo(new Vector3(3, 0, 3)));
            Assert.That(grid.GetCellCenterPosition(1, 2), Is.EqualTo(new Vector3(3, 0, 5)));

            Assert.That(grid.GetCellCenterPosition(2, 0), Is.EqualTo(new Vector3(5, 0, 1)));
            Assert.That(grid.GetCellCenterPosition(2, 1), Is.EqualTo(new Vector3(5, 0, 3)));
            Assert.That(grid.GetCellCenterPosition(2, 2), Is.EqualTo(new Vector3(5, 0, 5)));
        }
    }
}
