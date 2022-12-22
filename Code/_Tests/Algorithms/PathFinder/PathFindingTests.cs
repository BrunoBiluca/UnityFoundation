using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code.Algorithms.Tests
{
    public class PathFindingTests
    {
        [Test]
        public void Should_find_path_in_one_dimensional_grid()
        {
            var unidimensionalGrid = new PathFinding.GridSize(1, 3);
            var pathFinding = new PathFinding(unidimensionalGrid);

            var path = pathFinding.FindPath(new Int2(0, 0), new Int2(0, 2));

            var fullPath = path.ToList();

            Assert.That(fullPath.Count, Is.EqualTo(3));
            Assert.That(fullPath[0], Is.EqualTo(new Int2(0, 0)));
            Assert.That(fullPath[1], Is.EqualTo(new Int2(0, 1)));
            Assert.That(fullPath[2], Is.EqualTo(new Int2(0, 2)));
        }

        [Test]
        public void Should_not_find_path_when_position_is_blocked()
        {
            var unidimensionalGrid = new PathFinding.GridSize(1, 3);

            var pathFinding = new PathFinding(unidimensionalGrid);
            pathFinding.AddBlocked(new Int2(0, 1));

            var path = pathFinding.FindPath(new Int2(0, 0), new Int2(0, 2));

            var fullPath = path.ToList();

            Assert.That(fullPath.Count, Is.EqualTo(1));
            Assert.That(fullPath[0], Is.EqualTo(new Int2(-1, -1)));
        }

        [Test]
        public void Should_find_path_in_a_two_dimension_grid()
        {
            var unidimensionalGrid = new PathFinding.GridSize(2, 3);

            var pathFinding = new PathFinding(unidimensionalGrid);
            pathFinding.AddBlocked(new Int2(0, 1));

            Debug.Log(pathFinding.GetStringRepresentation());

            var path = pathFinding.FindPath(new Int2(0, 0), new Int2(0, 2));

            var fullPath = path.ToList();

            Assert.That(fullPath.Count, Is.EqualTo(3));
            Assert.That(fullPath[0], Is.EqualTo(new Int2(0, 0)));
            Assert.That(fullPath[1], Is.EqualTo(new Int2(1, 1)));
            Assert.That(fullPath[2], Is.EqualTo(new Int2(0, 2)));
        }
    }
}
