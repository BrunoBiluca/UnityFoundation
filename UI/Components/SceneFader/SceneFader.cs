using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityFoundation.Code;
using UnityFoundation.Code.Timer;

namespace Assets.UnityFoundation.SceneFader
{
    public class SceneFader : Singleton<SceneFader>
    {
        private static class SceneFaderAnimations
        {
            public const string fadeIn = "FadeIn";
            public const string fadeOut = "FadeOut";
        }

        [SerializeField]
        private GameObject fadeCanvas;

        [SerializeField]
        private Animator fadeAnim;

        protected override void OnAwake()
        {
            if(fadeCanvas == null)
                fadeCanvas = transform.Find("canvas").gameObject;

            if(fadeAnim == null)
                fadeAnim = fadeCanvas.transform.Find("panel").GetComponent<Animator>();
        }

        protected override void PreAwake()
        {
            DestroyOnLoad = false;
        }

        public void FadeIn(string levelName)
        {
            StartCoroutine(FadeInAnimation(levelName));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutAnimation());
        }

        IEnumerator FadeInAnimation(string sceneName)
        {
            fadeCanvas.SetActive(true);
            fadeAnim.Play(SceneFaderAnimations.fadeIn);
            yield return StartCoroutine(WaittingCoroutine.RealSeconds(0.7f));

            OnLoadScene(sceneName);

            FadeOut();
        }

        IEnumerator FadeOutAnimation()
        {
            fadeAnim.Play(SceneFaderAnimations.fadeOut);
            yield return StartCoroutine(WaittingCoroutine.RealSeconds(1.0f));
            fadeCanvas.SetActive(false);
        }

        protected virtual void OnLoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}