using UnityEngine;

namespace UnityFoundation.FirstPersonModeSystem
{
    public interface IFirstPersonInputs
    {
        bool Aim { get; }
        bool Fire { get; }
        bool Jump { get; }
        Vector2 Look { get; }
        Vector2 Move { get; }
        bool Reload { get; }

        void Disable();
        void Enable();
    }
}