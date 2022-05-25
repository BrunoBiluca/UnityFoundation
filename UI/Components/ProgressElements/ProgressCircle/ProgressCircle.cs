using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.ProgressElements.ProgressCircle
{
    public class ProgressCircle : MonoBehaviour
    {
        [SerializeField] private bool useCounter = true;
        [SerializeField] private float timerMax;

        private Image mask;
        private TextMeshProUGUI counterText;

        void Awake()
        {
            mask = transform.Find("mask").GetComponent<Image>();

            counterText = transform.Find("counter_text")
                .GetComponent<TextMeshProUGUI>();
            counterText.gameObject.SetActive(useCounter);
        }

        public ProgressCircle Setup(float timerMax)
        {
            this.timerMax = timerMax;
            return this;
        }

        public ProgressCircle Display(float timer)
        {
            Show();
            mask.fillAmount = timer / timerMax;
            return this;
        }

        public ProgressCircle DisplayCounter(int value)
        {
            if(!counterText.gameObject.activeInHierarchy)
                counterText.gameObject.SetActive(true);

            counterText.text = value.ToString();

            return this;
        }

        public void Hide()
        {
            if(!gameObject.activeInHierarchy)
                return;

            gameObject.SetActive(false);
        }

        private void Show()
        {
            if(gameObject.activeInHierarchy)
                return;

            gameObject.SetActive(true);
        }
    }
}