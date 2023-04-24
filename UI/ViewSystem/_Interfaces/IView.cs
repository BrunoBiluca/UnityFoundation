using System;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.UI.ViewSystem
{
    public interface IView : IVisible
    {
        event Action OnVisible;
        event Action<bool> OnVisibilityChanged;
        event Func<bool> CanBeShow;

        bool IsVisible { get; }
    }
}