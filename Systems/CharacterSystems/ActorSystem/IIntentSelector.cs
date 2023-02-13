using System;
using UnityFoundation.Code;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IIntentSelector<TIntent> where TIntent : IIntent
    {
        public event Action<TIntent> OnIntentSelected;
        public event Action OnIntentUnselected;

        public Optional<TIntent> CurrentIntent { get; }

        /// <summary>
        /// Set an action to be excecuted
        /// </summary>
        /// <param name="intent"></param>
        /// <exception cref="InvalidOperationException">The action can't be executed</exception>
        public void SetIntent(TIntent intent);
        void UnselectIntent();
    }
}