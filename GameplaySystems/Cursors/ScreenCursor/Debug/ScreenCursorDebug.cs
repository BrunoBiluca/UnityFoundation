using TMPro;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.Timer;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Cursors
{
    public class ScreenCursorDebug : MonoBehaviour
    {
        [SerializeField] private ScreenCursorMono cursorMono;

        [SerializeField] private GameObject allScreenPanel;
        [SerializeField] private GameObject dragReference;

        private ScreenCursor cursor;

        private Transform panel;
        private GameObject circle;
        private GameObject clickReference;
        private TextMeshProUGUI screenPositionText;
        private ITimer clickReferenceTimer;

        public void Start()
        {
            if(cursorMono.UsePanelAsPivot && cursorMono.Pivot != null)
                panel = cursorMono.Pivot.transform;
            else
                panel = allScreenPanel.transform;

            transform.SetParent(panel);

            circle = transform.Find("circle").gameObject;
            clickReference = transform.Find("click_reference").gameObject;
            screenPositionText = transform.FindComponent<TextMeshProUGUI>("screen_position");

            clickReferenceTimer = new Timer(.15f, HideClickReference).RunOnce();

            if(cursorMono != null)
                Setup(cursorMono.GetComponent<ComponentHolder<ScreenCursor>>().Instance);
        }

        public void Setup(ScreenCursor cursor)
        {
            this.cursor = cursor;
            cursor.OnClick += ShowClickReference;
            cursor.OnDrag += ShowDragReference;
        }

        private void ShowDragReference(CursorDrag obj)
        {
            var drag = Instantiate(dragReference, panel);
            drag.transform.localPosition = obj.StartPosition.Value.Get();
            drag.transform.RotateZ(obj.StartPosition.Value.Get(), obj.EndPosition.Value.Get());
            Destroy(drag, 4);
        }

        public void Update()
        {
            cursor?.ScreenPosition.Original.Some(p => {
                var debugPos = p;
                var screenPosText = $"SP: {p}";
                if(cursor.ScreenPosition.Value.IsPresentAndGet(out Vector2 cp))
                {
                    circle.SetActive(true);
                    screenPositionText.gameObject.SetActive(true);
                    screenPosText += $"\nCP: {cp}";
                    debugPos = cp;
                }
                else
                {
                    circle.SetActive(false);
                    screenPositionText.gameObject.SetActive(false);
                }

                screenPositionText.text = screenPosText;
                transform.localPosition = debugPos;
            });
        }

        private void ShowClickReference()
        {
            clickReference.SetActive(true);
            clickReferenceTimer.Start();
        }

        private void HideClickReference()
        {
            clickReference.SetActive(false);
        }

        private void OnDestroy()
        {
            cursor.OnClick -= ShowClickReference;
        }
    }
}
