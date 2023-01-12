using System;
using UnityFoundation.Code;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IActionSelector<TAction> where TAction : IIntent
    {
        public event Action<TAction> OnActionSelected;
        public event Action OnActionUnselected;

        public Optional<TAction> CurrentAction { get; }

        /// <summary>
        /// Set an action to be excecuted
        /// </summary>
        /// <param name="action"></param>
        /// <exception cref="InvalidOperationException">The action can't be executed</exception>
        public void SetAction(TAction action);
        void UnselectAction();
    }
}