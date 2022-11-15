using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFoundation.Code.DebugHelper
{
    public class DebuggerScreen : Singleton<DebuggerScreen>
    {
        [SerializeField] private GameObject actionButtonPrefab;

        private RectTransform debuggerView;
        private Button openButton;
        private Button closeButton;

        [field: SerializeField] public bool StartOpened { get; private set; }

        protected override void OnStart()
        {
            SetupComponents();

            SetupDebuggerActions();

            if(StartOpened) OpenDebugger();
            else CloseDebugger();
        }

        private void SetupComponents()
        {
            debuggerView = transform.FindComponent<RectTransform>("panel");

            openButton = transform.FindComponent<Button>("open_button");
            openButton.onClick.AddListener(OpenDebugger);

            closeButton = transform.FindComponent<Button>("panel.close_button");
            closeButton.onClick.AddListener(CloseDebugger);
        }

        private void OpenDebugger()
        {
            debuggerView.gameObject.SetActive(true);
            openButton.gameObject.SetActive(false);
        }

        private void CloseDebugger()
        {
            debuggerView.gameObject.SetActive(false);
            openButton.gameObject.SetActive(true);
        }

        private void SetupDebuggerActions()
        {
            var holder = transform.FindTransform("panel.holder");

            var actions = GetComponents<IDebuggerAction>();
            foreach(var action in actions)
            {
                var go = Instantiate(actionButtonPrefab, holder);

                go.GetComponent<Button>().onClick.AddListener(action.Execute);
                go.transform.FindComponent<TextMeshProUGUI>("text").text = action.Name;
            }
        }
    }
}
