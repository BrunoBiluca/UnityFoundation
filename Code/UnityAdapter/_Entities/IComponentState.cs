using System;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IComponentState
    {
        bool IsValid { get; }
        event Action OnInvalidState;
    }
}
