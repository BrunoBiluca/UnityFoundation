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
        }

        public void Start()
        {
            if(!Application.isPlaying) return;

            if(StartVisible) Show();
            else Hide();
        }

        private void InitView()
        {
            wasAwaken = true;
            firstShowCall = new(OnFirstShow);
            OnAwake();
        }

        /// <summary>
        /// Runs before any Awake logic (PlayMode and EditMode)
        /// </summary>
        protected virtual void PreAwake() { }

        /// <summary>
        /// Runs after all Awake logic
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        /// Runs when turn from Invisible to Visible
        /// </summary>
        protected virtual void OnShow() { }

        /// <summary>
        /// Runs when turn from Visible to Invisible
        /// </summary>
        protected virtual void OnHide() { }

        /// <summary>
        /// Runs the first time when is Visible
        /// </summary>
        protected virtual void OnFirstShow() { }
    }
}