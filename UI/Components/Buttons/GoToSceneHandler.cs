using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace UnityFoundation.UI
{

    public class GoToSceneHandler : MonoBehaviour
    {
        [field: SerializeField] public string SceneName { get; private set; }

        public void Awake()
        {
            if(TryGetComponent(out Button button))
            {
                button.onClick.AddListener(() => SceneManager.LoadScene(SceneName));
            }
        }
    }
}