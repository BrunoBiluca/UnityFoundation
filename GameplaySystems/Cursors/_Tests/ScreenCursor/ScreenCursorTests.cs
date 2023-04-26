using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Cursors.Tests
{
    public class ScreenCursorTests
    {
        [Test]
        public void Should_not_have_screen_position_when_was_not_updated()
        {
            var cursor = new ScreenCursor(new Mock<ICursorInput>().Object);

            Assert.That(cursor.ScreenPosition.Original.IsPresent, Is.False);
        }

        [Test]
        public void Should_have_screen_position_when_updated()
        {
            var cursorMock = new Mock<ICursorInput>();
            cursorMock.Setup(c => c.Position).Returns(new Vector2(1, 1));

            var cursor = new ScreenCursor(cursorMock.Object);
            cursor.Update();

            Assert.That(cursor.ScreenPosition.Original.IsPresent, Is.True);
        }

        [Test]
        public void Should_callback_on_release()
        {
            var screenPos = new Vector2(0, 1);

            var inputMock = new Mock<ICursorInput>();
            inputMock.SetupSequence(i => i.Position)
                .Returns(screenPos);

            var cursor = new ScreenCursor(inputMock.Object);

            var onReleasedTest = new EventTest(cursor, nameof(cursor.OnReleased));

            inputMock.Setup(i => i.WasPressed).Returns(false);
            inputMock.Setup(i => i.WasReleased).Returns(true);
            cursor.Update();

            Assert.That(onReleasedTest.WasTriggered, Is.True);
            Assert.That(cursor.ScreenPosition.Original.Get(), Is.EqualTo(screenPos));
        }

        public static IEnumerable<TestCaseData> Drag_source()
        => new TestCaseSourceBuilder()
            .Test("Drag",
                ("from", new Vector2(0, 0)),
                ("to", new Vector2(1, 0)),
                ("should have direction", new Vector2(1, 0))
            )
            .Test("Drag",
                ("from", new Vector2(1, 0)),
                ("to", new Vector2(0, 0)),
                ("should have direction", new Vector2(-1, 0))
            )
            .Test("Drag",
                ("from", new Vector2(2, 2)),
                ("to", new Vector2(2, 0)),
                ("should have direction", new Vector2(0, -1))
            )
            .Test("Drag",
                ("from", new Vector2(1, 1)),
                ("to", new Vector2(2, 2)),
                ("should have direction", new Vector2(1, 1))
            )
            .Build();


        [TestCaseSource(nameof(Drag_source))]
        public void Given_cursor_was_pressed_and_released_when(
            Vector2 clickPosition, Vector2 releasedPosition, Vector2 expected
        )
        {
            var inputMock = new Mock<ICursorInput>();
            inputMock.SetupSequence(i => i.Position)
                .Returns(clickPosition)
                .Returns(releasedPosition);

            var cursor = new ScreenCursor(inputMock.Object);

            var onClickTest = new EventTest(cursor, nameof(cursor.OnClick));
            var onDragTest = new EventTest<CursorDrag>(cursor, nameof(cursor.OnDrag));
            var onReleasedTest = new EventTest(cursor, nameof(cursor.OnReleased));

            // click
            inputMock.Setup(i => i.WasPressed).Returns(true);
            cursor.Update();
            var clickScreenPosition = cursor.ScreenPosition.Original.Get();

            // release
            inputMock.Setup(i => i.WasPressed).Returns(false);
            inputMock.Setup(i => i.WasReleased).Returns(true);
            cursor.Update();
            var releasedScreenPosition = cursor.ScreenPosition.Original.Get();

            Assert.That(onClickTest.WasTriggered, Is.True);
            Assert.That(onReleasedTest.WasTriggered, Is.True);
            Assert.That(onDragTest.WasTriggered, Is.True);

            Assert.That(onDragTest.Parameter.StartPosition.Value.Get(), Is.EqualTo(clickScreenPosition));
            Assert.That(onDragTest.Parameter.EndPosition.Value.Get(), Is.EqualTo(releasedScreenPosition));
            Assert.That(onDragTest.Parameter.Direction, Is.EqualTo(expected.normalized));
        }


        public static IEnumerable<TestCaseData> Drag_distance_source()
        => new TestCaseSourceBuilder()
            .Test("Drag from (0, 0) to (2, 0)", "with distance", 1f, "should be triggered", true)
            .Test("Drag", "with distance", 1.9f, "should not be triggered", true)
            .Test("Drag", "with distance", 2f, "should be triggered", true)
            .Test("Drag", "with distance", 2.1f, "should be triggered", false)
            .Test("Drag", "with distance", 3f, "should be triggered", false)
            .Build();

        [TestCaseSource(nameof(Drag_distance_source))]
        public void Drag_event_trigger_behaviour(float distance, bool expectedTrigger)
        {
            var inputMock = new Mock<ICursorInput>();
            inputMock.SetupSequence(i => i.Position)
                .Returns(Vector2.zero)
                .Returns(new Vector2(2f, 0));

            var cursor = new ScreenCursor(inputMock.Object) { MinDragDistance = distance };

            var onDragTest = new EventTest<CursorDrag>(cursor, nameof(cursor.OnDrag));

            inputMock.Setup(i => i.WasPressed).Returns(true);
            cursor.Update();

            inputMock.Setup(i => i.WasPressed).Returns(false);
            inputMock.Setup(i => i.WasReleased).Returns(true);
            cursor.Update();

            Assert.That(onDragTest.WasTriggered, Is.EqualTo(expectedTrigger));
        }
    }
}