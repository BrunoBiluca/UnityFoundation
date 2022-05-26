using TMPro;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public class TextCounterHealthBar : MonoBehaviour, IHealthBar
    {
        [SerializeField] private TMP_Text maxText;
        [SerializeField] private TMP_Text currentText;

        private float baseHealth;
        private float currentHealth;

        void Awake()
        {
            if(currentText == null)
            {
                currentText = transform.Find("current_text").GetComponent<TMP_Text>();
                maxText = transform.Find("max_text").GetComponent<TMP_Text>();
            }
        }

        public void Setup(float baseHealth)
        {
            this.baseHealth = baseHealth;
            currentHealth = baseHealth;
            currentText.text = Mathf.FloorToInt(currentHealth).ToString();
            maxText.text = Mathf.FloorToInt(this.baseHealth).ToString();
        }

        public void SetCurrentHealth(float currentHealth)
        {
            this.currentHealth = Mathf.Clamp(currentHealth, 0, baseHealth);
            currentText.text = Mathf.FloorToInt(this.currentHealth).ToString();
            maxText.text = Mathf.FloorToInt(baseHealth).ToString();
        }
    }
}