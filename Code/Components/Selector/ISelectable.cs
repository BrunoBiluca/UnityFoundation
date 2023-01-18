using System;

namespace UnityFoundation.Code
{
    public interface ISelectable
    {
        event Action OnSelected;
        event Action OnUnselected;
        event Action OnSelectedStateChange;

        bool IsSelected { get; }
        object SelectedReference { get; }

        void SetSelected(bool state);
    }
}