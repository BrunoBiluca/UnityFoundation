using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.GameManagers {
    public class BaseGameManager : MonoBehaviour {

        private static BaseGameManager instance;
        public static BaseGameManager Instance {
            get {
                if(instance == null) {
                    instance = new GameObject("BaseGameManager").AddComponent<BaseGameManager>();
                    instance.DebugMode = true;
                }
                return instance;
            }
            private set { instance = value; }
        }

        [SerializeField]
        private bool debugMode;
        public bool DebugMode {
            get {
                return debugMode;
            }
            private set {
                debugMode = value;
            }
        }

        public void Awake() {
            Instance = this;
        }

    }
}