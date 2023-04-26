using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Cursors.Tests
{
    public class ConverterGroupTests
    {
        [Test]
        public void Pivot_and_BoundariesChecker_behaviour()
        {
            var cursorMock = new Mock<ICursorInput>();
            cursorMock.SetupSequence(c => c.Position)
                .Returns(new Vector2(3, 3))
                .Returns(new Vector2(1, 3));

            var group = new ScreenPositionConverterGroup(new List<IScreenPositionConverter> {
                new BoundariesChecker(new Rect(2, 2, 3, 3)),
                new PivotConverter(new Vector2(2, 2))
            });

            var cursor = new ScreenCursor(cursorMock.Object);
            cursor.SetConverter(group);

            cursor.Update();
            Assert.That(cursor.ScreenPosition.Value.IsPresent, Is.True);
            Assert.That(cursor.ScreenPosition.Value.Get(), Is.EqualTo(new Vector2(1, 1)));

            cursor.Update();
            Assert.That(cursor.ScreenPosition.Value.IsPresent, Is.False);
        }
    }
}
