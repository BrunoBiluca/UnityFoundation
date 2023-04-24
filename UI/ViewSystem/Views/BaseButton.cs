using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFoundation.UI.ViewSystem
{
    public abstract class BaseButton : BaseView
    {
        protected Button Button { get; private set; }
        protected TextMeshProUGUI Label { get; private set; }

        protected override void OnAwake()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(ClickHandler);

            Label = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected abstract void ClickHandler();
    }
}