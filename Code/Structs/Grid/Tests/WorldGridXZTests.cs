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
            yield return new TestCaseData(Vector3.zero).SetName("Zero");
            yield return new TestCaseData(Vector3.one).SetName("One");
            yield return new TestCaseData(-Vector3.one).SetName("Minus One");
        }

        [Test]
        [TestCaseSource(nameof(MultipleInitialPositions))]
        public void Given_filled_grid_should_return_value_if_world_position_inside_grid(
            Vector3 initialPosition
        )
        {
            var grid = new WorldGridXZ<string>(initialPosition, 2, 2, 1);
            grid.Fill("filled");

            var pos00 = new Vector3(0, 0, 0) + initialPosition;
            var pos01 = new Vector3(0, 0, 1) + initialPosition;
            var pos10 = new Vector3(1, 0, 0) + initialPosition;
            var pos11 = new Vector3(1, 0, 1) + initialPosition;

            Assert.AreEqual(pos00, grid.GetCellWorldPosition(pos00));
            Assert.AreEqual(pos01, grid.GetCellWorldPosition(pos01));
            Assert.AreEqual(pos10, grid.GetCellWorldPosition(pos10));
            Assert.AreEqual(pos11, grid.GetCellWorldPosition(pos11));
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

    }
}
