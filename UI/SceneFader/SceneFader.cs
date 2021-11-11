using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.UnityFoundation.TimeUtils;
using Assets.UnityFoundation.Code.Common;

namespace Assets.UnityFoundation.SceneFader {
    public class SceneFader : Singleton<SceneFader> {

        public override bool DestroyOnLoad => false;

        private static class SceneFaderAnimations{
            public const string fadeIn = "FadeIn";
            public const string fadeOut = "FadeOut";
        }

        [SerializeField]
        private GameObject fadeCanvas;

        [SerializeField]
        private Animator fadeAnim;

        public void FadeIn(string levelName) {
            StartCoroutine(FadeInAnimation(levelName));
        }

        public void FadeOut() {
            StartCoroutine(FadeOutAnimation());
        }

        IEnumerator FadeInAnimation(string levelName) {
            fadeCanvas.SetActive(true);
            fadeAnim.Play(SceneFaderAnimations.fadeIn);
            yield return StartCoroutine(WaittingCoroutine.RealSeconds(0.7f));
            SceneManager.LoadScene(levelName);
            FadeOut();
        }

        IEnumerator FadeOutAnimation() {
            fadeAnim.Play(SceneFaderAnimations.fadeOut);
            yield return StartCoroutine(WaittingCoroutine.RealSeconds(1.0f));
            fadeCanvas.SetActive(false);
        }

    }
}