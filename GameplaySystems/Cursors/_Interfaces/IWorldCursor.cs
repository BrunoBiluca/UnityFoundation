using System;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.WorldCursors
{
    public interface IWorldCursor
    {
        event Action OnClick;
        event Action OnSecondaryClick;

        Optional<Vector3> WorldPosition { get; }
        Optional<Vector2> ScreenPosition { get; }

        void Update();
        void Enable();
        void Disable();
    }
}