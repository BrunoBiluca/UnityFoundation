using UnityEngine;

namespace Assets.UnityFoundation.DamagePopup.Scripts {
    public class DamagePopupAssetsManager : MonoBehaviour {

        private static DamagePopupAssetsManager instance;
        public static DamagePopupAssetsManager Instance {
            get {
                if(instance == null) instance = FindObjectOfType<DamagePopupAssetsManager>();
                return instance;
            }
        }

        public GameObject DamagePopup { get; private set; }
    }
}