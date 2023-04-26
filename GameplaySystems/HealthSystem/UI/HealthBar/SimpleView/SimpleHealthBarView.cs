using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.HealthSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class SimpleHealthBarView : MonoBehaviour, IHealthBar
    {
        private RectTransform baseRect;
        private RectTransform progressBar;
        private float baseHealth;
        private float currHealth;

        public void Awake()
        {
            if(!TryGetComponent(out baseRect))
                throw new MissingComponentException();

            progressBar = transform.FindComponent<RectTransform>("bar", "bar_progress");
            if(progressBar == null)
                throw new MissingComponentException();
        }

        public void Setup(float baseHealth)
        {
            this.baseHealth = baseHealth;
            currHealth = baseHealth;

            if(progressBar != null) Awake();
            progressBar.sizeDelta = new Vector2(0, 0);
        }

        public void SetCurrentHealth(float currentHealth)
        {
            currHealth = currentHealth;

            var healthRatio = 1f - currHealth / baseHealth;

            progressBar.sizeDelta = new Vector2(
                -healthRatio * baseRect.rect.width,
                progressBar.sizeDelta.y
            );
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}