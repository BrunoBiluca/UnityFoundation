using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFoundation.Cursors
{
    public class MouseInput : ICursorInput
    {
        public Vector2 Position => Mouse.current.position.ReadValue();
        public bool WasPressed => Mouse.current.leftButton.wasPressedThisFrame;
        public bool WasReleased => Mouse.current.leftButton.wasReleasedThisFrame;
    }
}