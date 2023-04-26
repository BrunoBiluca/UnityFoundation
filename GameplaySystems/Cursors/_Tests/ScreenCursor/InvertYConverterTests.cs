using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Cursors.Tests
{
    public class InvertYConverterTests
    {
        public static IEnumerable<TestCaseData> Screen_position_source()
        => new TestCaseSourceBuilder()
            .Meta("pivot (0, 0) with screen height 2", new Vector2(0, 2))
            .Test("upper left", new Vector2(0, 2), new Vector2(0, 0))
            .Test("middle left", new Vector2(0, 1), new Vector2(0, -1))
            .Test("middle right", new Vector2(2, 1), new Vector2(2, -1))
            .Test("bottom right", new Vector2(2, 0), new Vector2(2, -2))
            .Meta("pivot (1, 1) with screen height 2", new Vector2(1, 1))
            .Test("middle left", new Vector2(1, 1), new Vector2(0, 0))
            .Test("upper left", new Vector2(0, 2), new Vector2(-1, 1))
            .Test("middle right", new Vector2(2, 1), new Vector2(1, 0))
            .Test("bottom right", new Vector2(2, 0), new Vector2(1, -1))
            .Meta("pivot (1, 3) with screen height 5", new Vector2(1, 3))
            .Test("that",
                "screen position", new Vector2(1, 3),
                "is converted to", new Vector2(0, 0)
            )
        .Build();

        [TestCaseSource(nameof(Screen_position_source))]
        public void Should_convert_screen_position_when(
            Vector2 pivot, Vector2 screen, Vector2 expected
        )
        {
            var inputMock = new Mock<ICursorInput>();
            inputMock.Setup(i => i.Position).Returns(screen);

            var cursor = new ScreenCursor(inputMock.Object);
            cursor.SetConverter(new PivotConverter(pivot));

            cursor.Update();
            Assert.That(cursor.ScreenPosition.Value.Get(), Is.EqualTo(expected));
        }
    }
}
