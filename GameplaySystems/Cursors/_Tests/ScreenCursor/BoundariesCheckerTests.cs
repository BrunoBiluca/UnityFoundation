using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Cursors.Tests
{
    public class BoundariesCheckerTests
    {
        public static IEnumerable<TestCaseData> Boundaries_cases()
        => new TestCaseSourceBuilder()
            .Meta("return value", true)
            .Test("Should", "position", new Vector2(5, 5), "is inside rect", new Rect(0, 0, 9, 9))
            .Meta("not return value", false)
            .Test("Should", "position", new Vector2(10, 10), "is inside rect", new Rect(0, 0, 9, 9))
            .Test("Should", "position", new Vector2(-1, -1), "is inside rect", new Rect(0, 0, 9, 9))
            .Build();


        [TestCaseSource(nameof(Boundaries_cases))]
        public void BondariesChecker_behaviour(
            bool expectedToBePresent, Vector2 position, Rect container
        )
        {
            var inputMock = new Mock<ICursorInput>();
            inputMock.Setup(i => i.Position).Returns(position);

            var cursor = new ScreenCursor(inputMock.Object);
            cursor.SetConverter(new BoundariesChecker(container));

            cursor.Update();
            Assert.That(cursor.ScreenPosition.Value.IsPresent, Is.EqualTo(expectedToBePresent));

            if(expectedToBePresent)
                Assert.That(cursor.ScreenPosition.Value.Get(), Is.EqualTo(position));
        }

        public static IEnumerable<TestCaseData> Boundaries_with_direction_cases()
        => new TestCaseSourceBuilder()
            .Meta("return value", true)
            .Test("Should", "position", new Vector2(5, 5), "is inside rect", new Rect(0, 10, 6, 6))
            .Meta("not return value", false)
            .Test("Should", "position", new Vector2(0, 0), "is inside rect", new Rect(0, 10, 9, 9))
            .Build();

        [TestCaseSource(nameof(Boundaries_with_direction_cases))]
        public void BondariesChecker_behaviour_with_different_direction(
            bool expectedToBePresent, Vector2 position, Rect container
        )
        {
            var inputMock = new Mock<ICursorInput>();
            inputMock.Setup(i => i.Position).Returns(position);

            var cursor = new ScreenCursor(inputMock.Object);
            cursor.SetConverter(new BoundariesChecker(container) { Direction = new(1, -1) });

            cursor.Update();
            Assert.That(cursor.ScreenPosition.Value.IsPresent, Is.EqualTo(expectedToBePresent));

            if(expectedToBePresent)
                Assert.That(cursor.ScreenPosition.Value.Get(), Is.EqualTo(position));
        }
    }
}
