using NUnit.Framework;
using System;
using UnityFoundation.Code.Grid;

namespace UnityFoundation.Code.Tests
{
    public class GridXZTests
    {
        [Test]
        public void ShouldInitializeGridEmpty()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            var gridPos = grid.GetGridPosition(0, 0);

            Assert.AreEqual(0, gridPos.X);
            Assert.AreEqual(0, gridPos.Z);
        }

        [Test]
        public void ShouldThrowErrorWhenGetPositionOutsideGrid()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetGridPosition(4, 4));
        }

        [Test]
        public void ShouldNotSetValueWhenGridPositionHasValue()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.IsFalse(grid.TrySetValue(2, 2, 432));

            Assert.IsTrue(grid.TrySetValue(0, 0, 123));
            Assert.IsFalse(grid.TrySetValue(0, 0, 987));
        }

        [Test]
        public void ShouldSetValueWhenGridPositionHasValueAndForceActive()
        {
            var grid = new GridXZ<int>(4, 4, 1) {
                ForceSetValue = true
            };

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.IsTrue(grid.TrySetValue(2, 2, 432));
        }

        [Test]
        public void ShouldSetValueWhenGridPositionWasCleared()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.IsFalse(grid.TrySetValue(2, 2, 432));

            grid.ClearValue(2, 2);

            Assert.IsTrue(grid.TrySetValue(2, 2, 432));
        }

        [Test]
        public void ShouldReturnValueWhenPositionInsideGrid()
        {
            var grid = new GridXZ<int>(4, 4, 1);

            Assert.IsTrue(grid.TrySetValue(2, 2, 123));
            Assert.AreEqual(123, grid.GetValue(2, 2));
        }

        [Test]
        public void ShouldSetValueInAllGridPositions()
        {
            var grid = new GridXZ<int>(2, 2, 1);

            grid.Fill(123);

            Assert.AreEqual(123, grid.GetValue(0, 0));
            Assert.AreEqual(123, grid.GetValue(0, 1));
            Assert.AreEqual(123, grid.GetValue(1, 0));
            Assert.AreEqual(123, grid.GetValue(1, 1));
        }
    }
}