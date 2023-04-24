using UnityEngine;
using UnityFoundation.UI.ViewSystem;

namespace UnityFoundation.UI
{
    public class ViewControllerButton : BaseButton
    {
        [SerializeField] private BaseView controlledView;
        [field: SerializeField] public string OpenLabel { get; private set; }
        [field: SerializeField] public string CloseLabel { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            UpdateLabel(controlledView.IsVisible);
            controlledView.OnVisibilityChanged += UpdateLabel;
        }

        protected override void ClickHandler()
        {
            if(controlledView.IsVisible)
                controlledView.Hide();
            else
                controlledView.Show();
        }

        private void UpdateLabel(bool state) => Label.text = !state ? OpenLabel : CloseLabel;
    }
}
