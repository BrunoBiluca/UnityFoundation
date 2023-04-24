using System;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.UI.ViewSystem
{
    public class ViewDecorator : IView
    {
        private readonly IGameObject refGameObject;

        public event Action OnVisible;
        public event Func<bool> CanBeShow;
        public event Action<bool> OnVisibilityChanged;

        public bool IsVisible => refGameObject.IsActiveInHierarchy;

        public bool StartVisible { get; set; }

        public ViewDecorator(IGameObject gameObject)
        {
            refGameObject = gameObject;
            refGameObject.SetActive(StartVisible);
        }

        public void Hide()
        {
            TryChangeVisibility(false);
        }

        public void Show()
        {
            if(CanShow()) return;

            if(TryChangeVisibility(true))
                OnVisible?.Invoke();
        }

        private bool TryChangeVisibility(bool state)
        {
            if(refGameObject.IsActiveInHierarchy == state)
                return false;

            OnVisibilityChanged?.Invoke(state);
            refGameObject.SetActive(state);
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
    }
}