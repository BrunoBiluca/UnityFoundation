using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityFoundation.UI
{
    public class ReloadSceneAction : MonoBehaviour, IGameOverAction
    {
        public string Name { get; private set; } = "Reload game";
        private string sceneName;

        public void Awake()
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        public void Execute()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}