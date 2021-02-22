using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.UnityFoundation.SceneFader {
    public class SceneFader : MonoBehaviour {
        public static SceneFader Instance { get; private set; }

        private static class SceneFaderAnimations{
            public const string fadeIn = "FadeIn";
            public const string fadeOut = "FadeOut";
        }

        [SerializeField]
        private GameObject fadeCanvas;

        [SerializeField]
        private Animator fadeAnim;

        void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void FadeIn(string levelName) {
            StartCoroutine(FadeInAnimation(levelName));
        }

        public void FadeOut() {
            StartCoroutine(FadeOutAnimation());
        }

        IEnumerator FadeInAnimation(string levelName) {
            fadeCanvas.SetActive(true);
            fadeAnim.Play(SceneFaderAnimations.fadeIn);
            yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.7f));
            SceneManager.LoadScene(levelName);
            FadeOut();
        }

        IEnumerator FadeOutAnimation() {
            fadeAnim.Play(SceneFaderAnimations.fadeOut);
            yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.0f));
            fadeCanvas.SetActive(false);
        }

    }
}