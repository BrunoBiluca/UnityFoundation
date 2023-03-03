using System;
using UnityFoundation.Code;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public partial interface IActor<TIntent> where TIntent : IIntent
    {
        event Action OnCantExecuteAction;
        event Action OnActionFinished;
        event Action OnActionExecuted;

        void Execute();
        void Set(TIntent action);
        void UnsetAction();
    }
}