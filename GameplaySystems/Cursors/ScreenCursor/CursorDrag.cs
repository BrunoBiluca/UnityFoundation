using UnityEngine;

namespace UnityFoundation.Cursors
{
    public struct CursorDrag
    {
        public ScreenPosition StartPosition { get; private set; }
        public ScreenPosition EndPosition { get; private set; }
        public Vector2 Direction { get; private set; }

        public CursorDrag(
            ScreenPosition startPosition,
            ScreenPosition endPosition,
            Vector2 direction
        )
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Direction = direction;
        }
    }
}