using System;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IAction
    {
        event Action OnCantExecuteAction;
        event Action OnFinishAction;

        void Execute();
    }
}
