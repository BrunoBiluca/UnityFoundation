using System;
using UnityFoundation.Code;

namespace UnityFoundation.Cursors
{
    public interface IScreenCursor : IUpdatable
    {
        event Action OnClick;
        event Action OnReleased;
        event Action<CursorDrag> OnDrag;

        float MinDragDistance { get; set; }
        ScreenPosition ScreenPosition { get; }

        void Enable();
        void Disable();
    }
}