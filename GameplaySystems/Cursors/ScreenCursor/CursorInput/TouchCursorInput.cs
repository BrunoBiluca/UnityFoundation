using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFoundation.Cursors
{
    public class TouchCursorInput : ICursorInput
    {
        public Vector2 Position => Touchscreen.current.primaryTouch.position.ReadValue();

        public bool WasPressed
            => Touchscreen.current.primaryTouch.phase.ReadValue()
            == UnityEngine.InputSystem.TouchPhase.Began;

        public bool WasReleased =>
            Touchscreen.current.primaryTouch.phase.ReadValue()
            == UnityEngine.InputSystem.TouchPhase.Ended;
    }
}