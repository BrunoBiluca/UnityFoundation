using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.UnityFoundation.PostProcessing {
    public class ChromaticAberrationHandler : MonoBehaviour {

        public static ChromaticAberrationHandler Instance;

        private Volume chromaticAberration;
        private float weight;

        void Awake() {
            Instance = this;
            chromaticAberration = GetComponent<Volume>();
        }

        private void Update() {
            chromaticAberration.weight = weight;
            if(weight > 0f) {
                weight -= 1f * Time.deltaTime;
            }
        }

        public void FullWeight() {
            weight = 1f;
        }
    }
}