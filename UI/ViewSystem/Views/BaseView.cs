using System;
using UnityEngine;

namespace UnityFoundation.UI.ViewSystem
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        [field: SerializeField] public bool StartVisible { get; set; } = true;

        public bool IsVisible => gameObject.activeInHierarchy;
        public event Action OnVisible;
        public event Func<bool> CanBeShow;
        public event Action<bool> OnVisibilityChanged;

        private bool wasAwaken = false;
        private CallOnce firstShowCall;

        public void Hide()
        {
            if(TryChangeVisibility(false))
                OnHide();
        }

        public void Show()
        {
            if(!CanShow()) return;

            if(!wasAwaken) InitView();

            firstShowCall.Call();
            if(!TryChangeVisibility(true))
                return;

            OnShow();
            OnVisible?.Invoke();
        }

        private bool TryChangeVisibility(bool state)
        {
            if(gameObject.activeInHierarchy == state)
                return false;

            OnVisibilityChanged?.Invoke(state);
            gameObject.SetActive(state);
            return true;
        }

        private bool CanShow()
        {
            if(CanBeShow != null)
                foreach(Func<bool> canShow in CanBeShow.GetInvocationList())
                    if(!canShow())
                        return false;

            return true;
        }

        public void Awake()
        {
            PreAwake();

            if(!Application.isPlaying) return;

            if(wasAwaken) return;

            InitView();

            if(StartVisible) Show();
            else Hide();
        }

        private void InitView()
        {
            wasAwaken = true;
            firstShowCall = new(OnFirstShow);
            OnAwake();
        }

        protected virtual void PreAwake() { }
        protected virtual void OnAwake() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
        protected virtual void OnFirstShow() { }
    }
}