using System;
using UnityFoundation.Code;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IActionSelector<TAction> where TAction : IActionIntent
    {
        public event Action<TAction> OnActionSelected;
        public event Action OnActionUnselected;

        public Optional<TAction> CurrentAction { get; }

        public void SetAction(TAction action);
        void UnselectAction();
    }
}