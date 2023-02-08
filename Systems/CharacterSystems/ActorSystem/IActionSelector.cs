using System;
using UnityFoundation.Code;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IActionSelector<TIntent> where TIntent : IIntent
    {
        public event Action<TIntent> OnActionSelected;
        public event Action OnActionUnselected;

        public Optional<TIntent> CurrentAction { get; }

        /// <summary>
        /// Set an action to be excecuted
        /// </summary>
        /// <param name="action"></param>
        /// <exception cref="InvalidOperationException">The action can't be executed</exception>
        public void SetIntent(TIntent action);
        void UnselectAction();
    }
}