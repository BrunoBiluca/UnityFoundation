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
            baseRect = GetComponent<RectTransform>();
            baseRect = baseRect != null ? baseRect : throw new MissingComponentException();

            progressBar = transform.FindComponent<RectTransform>("bar", "bar_progress");
            progressBar = progressBar != null ? progressBar : throw new MissingComponentException();
        }

        public void Setup(float baseHealth)
        {
            this.baseHealth = baseHealth;
            currHealth = baseHealth;

            progressBar.sizeDelta = new Vector2(baseRect.sizeDelta.x, progressBar.sizeDelta.y);
        }

        public void SetCurrentHealth(float currentHealth)
        {
            currHealth = currentHealth;

            progressBar.sizeDelta = new Vector2(
                currHealth / baseHealth * baseRect.sizeDelta.x,
                progressBar.sizeDelta.y
            );
        }
    }
}