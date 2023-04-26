using UnityEngine;

namespace UnityFoundation.Cursors
{
    public interface ICursorInput
    {
        Vector2 Position { get; }
        bool WasPressed { get; }
        bool WasReleased { get; }
    }
}