using System;
using UnityFoundation.Code;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public partial interface IActor<TFactory> where TFactory : IIntent
    {
        event Action OnCantExecuteAction;
        event Action OnActionFinished;

        void Execute();
        void Set(TFactory action);
        void UnsetAction();
    }
}