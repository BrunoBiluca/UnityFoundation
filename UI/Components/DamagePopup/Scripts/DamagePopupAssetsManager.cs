using UnityEngine;

namespace Assets.UnityFoundation.DamagePopup.Scripts {
    public class DamagePopupAssetsManager : MonoBehaviour {

        private static DamagePopupAssetsManager instance;
        public static DamagePopupAssetsManager Instance {
            get {
                if(instance == null) 
                    instance = FindObjectOfType<DamagePopupAssetsManager>();
                return instance;
            }
        }

        [field: SerializeField] public GameObject DamagePopupPrefab { get; private set; }
    }
}