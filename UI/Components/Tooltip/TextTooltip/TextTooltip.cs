using TMPro;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.UI
{
    public class TextTooltip : AbstractPointerTooltip
    {
        [field: SerializeField]
        public string Content { get; private set; }

        private TextMeshProUGUI text;

        protected override void UpdateTooltip(GameObject tooltipGO)
        {
            if(text == null)
                text = tooltipGO.transform.FindComponent<TextMeshProUGUI>("text");

            text.text = Content;
        }

        public void SetContentText(string text)
        {
            Content = text;
        }
    }
}
