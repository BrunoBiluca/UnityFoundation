using UnityFoundation.TestUtility;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFoundation.FirstPersonModeSystem.Tests
{
    public class FirstPersonModeInputsTest : InputTestFixture
    {
        [Test]
        public void ShouldReceiveInputValues_WhenInputsActionsEnabled()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            var inputs = new FirstPersonInputs(new FirstPersonInputActions());
            inputs.Enable();

            Press(keyboard.wKey);
            Assert.That(inputs.Move, Is.EqualTo(new Vector2(0f, 1f)));
            Release(keyboard.wKey);

            Press(keyboard.sKey);
            Assert.That(inputs.Move, Is.EqualTo(new Vector2(0f, -1f)));
            Release(keyboard.sKey);

            Press(keyboard.aKey);
            Assert.That(inputs.Move, Is.EqualTo(new Vector2(-1f, 0f)));
            Release(keyboard.aKey);

            Press(keyboard.dKey);
            Assert.That(inputs.Move, Is.EqualTo(new Vector2(1f, 0f)));
            Release(keyboard.dKey);

            Press(keyboard.wKey);
            Press(keyboard.aKey);

            var expected = new Vector2(-1f, 1f).normalized;
            AssertHelper.AreEqual(inputs.Move, expected, 0.0001f);

            Release(keyboard.wKey);
            Release(keyboard.aKey);
        }

        [Test]
        public void ShouldNotReceiveInputValues_WhenInputsActionsDisabled()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            var inputs = new FirstPersonInputs(new FirstPersonInputActions());
            inputs.Enable();

            Press(keyboard.wKey);
            Assert.That(inputs.Move, Is.EqualTo(new Vector2(0f, 1f)));
            Release(keyboard.wKey);

            inputs.Disable();

            Press(keyboard.wKey);
            Assert.That(inputs.Move, Is.EqualTo(new Vector2(0f, 0f)));
            Release(keyboard.wKey);
        }
    }
}