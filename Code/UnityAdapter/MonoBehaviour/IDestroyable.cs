using System;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IDestroyable
    {
        event Action OnDestroyAction;
    }
}