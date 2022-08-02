using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.Grid.Tests
{
    public class WorldGridXZTests
    {
        [Test]
        public void Given_filled_grid_should_return_value_if_world_position_inside_grid()
        {
            var grid = new WorldGridXZ<string>(2, 2, 1);
            grid.Fill("filled");

            var pos00 = new Vector3(0, 0, 0);
            var pos01 = new Vector3(0, 0, 1);
            var pos10 = new Vector3(1, 0, 0);
            var pos11 = new Vector3(1, 0, 1);

            Assert.AreEqual(pos00, grid.GetCellWorldPosition(pos00));
            Assert.AreEqual(pos01, grid.GetCellWorldPosition(pos01));
            Assert.AreEqual(pos10, grid.GetCellWorldPosition(pos10));
            Assert.AreEqual(pos11, grid.GetCellWorldPosition(pos11));
        }

    }
}
