using System;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class GameObjectDecorator : IGameObject
    {
        private readonly GameObject gameObject;

        public event Action OnInvalidState;

        public GameObjectDecorator(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public string Name => Ref((g) => g.name);

        public bool IsActiveInHierarchy => Ref((g) => g.activeInHierarchy);

        public bool IsValid { get; private set; }

        public void SetActive(bool state) => Ref((g) => g.SetActive(state));

        private void Ref(Action<GameObject> call)
        {
            try
            {
                call(gameObject);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
            }
        }

        private T Ref<T>(Func<GameObject, T> call)
        {
            try
            {
                return call(gameObject);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
                return default;
            }
        }

        private void SetInvalidState()
        {
            IsValid = false;
            OnInvalidState?.Invoke();
        }
    }
}
