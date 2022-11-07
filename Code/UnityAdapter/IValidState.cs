using System;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IValidState
    {
        bool IsValid { get; }
        event Action OnInvalidState;
    }
}
