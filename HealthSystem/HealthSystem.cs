using System;
using UnityEngine;

namespace Assets.UnityFoundation.HealthSystem {
    public class HealthSystem : MonoBehaviour, IDamageable {

        [SerializeField]
        private HealthBar healthBar;

        private float baseHealth;

        [SerializeField]
        private float currentHealth;
        public float CurrentHealth { 
            get { return currentHealth; }
            private set { currentHealth = value; } 
        }

        public EventHandler OnTakeDamage;
        public EventHandler OnFullyHeal;
        public EventHandler OnDied;

        public void Awake() {
            healthBar = transform.Find("healthBar").GetComponent<HealthBar>();
        }

        public void Setup(float baseHealth) {
            this.baseHealth = baseHealth;
            CurrentHealth = baseHealth;

            healthBar.Setup(baseHealth);

            OnFullyHeal?.Invoke(this, EventArgs.Empty);
        }


        public void Damage(float amount) {
            CurrentHealth -= amount;

            if(healthBar != null) {
                healthBar.SetSize(CurrentHealth);
            }

            if(CurrentHealth <= 0f) {
                OnDied?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
                return;
            }

            if(CurrentHealth < baseHealth) {
                OnTakeDamage?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Heal(float amount) {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, baseHealth);

            if(healthBar != null) {
                healthBar.SetSize(CurrentHealth);
            }

            if(CurrentHealth == baseHealth) {
                OnFullyHeal?.Invoke(this, EventArgs.Empty);
            }
        }

        public void HealFull() {
            CurrentHealth = baseHealth;

            if(healthBar != null) {
                healthBar.SetSize(CurrentHealth);
            }

            OnFullyHeal?.Invoke(this, EventArgs.Empty);
        }
    }
}