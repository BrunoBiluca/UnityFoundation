using UnityEngine;
using UnityEngine.UI;

namespace UnityFoundation.Code.Actions
{
    public class ExitGameHandler : MonoBehaviour
    {
        private void Awake()
        {
            if(TryGetComponent(out Button button))
            {
                button.onClick.AddListener(() => {
                    Application.Quit();
                });
            }
        }
    }
}