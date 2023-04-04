using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityFoundation.UI
{
    public class ReloadSceneHandler : MonoBehaviour
    {
        private Button reloadButton;

        void Start()
        {
            reloadButton = GetComponent<Button>();

            reloadButton.onClick.AddListener(ReloadScene);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}