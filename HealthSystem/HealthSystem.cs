using System;
using UnityEngine;

namespace Assets.UnityFoundation.HealthSystem {
    public class HealthSystem : MonoBehaviour, IDamageable {

        // TODO: criar aqui uma annotation de restrição
        // para elementos que possui um component IHealthBar
        [SerializeField]
        private GameObject healthBarComponent;

        private IHealthBar healthBar;

        [SerializeField]
        private float baseHealth;

        [SerializeField]
        private bool destroyHealthBarOnDied = false;

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
            Setup(baseHealth);
        }

        private void HealthBarReference() {
            if(healthBar == null && healthBarComponent != null) {
                healthBar = healthBarComponent.GetComponent<IHealthBar>();
                return;
            }

            if(healthBar == null) {
                healthBarComponent = transform.Find("healthBar").gameObject;
                healthBar = healthBarComponent.GetComponent<IHealthBar>();
            }
        }

        public void Setup(float baseHealth) {
            this.baseHealth = baseHealth;
            CurrentHealth = baseHealth;

            HealthBarReference();
            healthBar.Setup(baseHealth);

            OnFullyHeal?.Invoke(this, EventArgs.Empty);
        }


        public void Damage(float amount) {
            CurrentHealth -= amount;

            if(healthBar != null) {
                healthBar.SetCurrentHealth(CurrentHealth);
            }

            if(CurrentHealth <= 0f) {
                OnDied?.Invoke(this, EventArgs.Empty);

                if(destroyHealthBarOnDied) Destroy(healthBarComponent);
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
                healthBar.SetCurrentHealth(CurrentHealth);
            }

            if(CurrentHealth == baseHealth) {
                OnFullyHeal?.Invoke(this, EventArgs.Empty);
            }
        }

        public void HealFull() {
            CurrentHealth = baseHealth;

            if(healthBar != null) {
                healthBar.SetCurrentHealth(CurrentHealth);
            }

            OnFullyHeal?.Invoke(this, EventArgs.Empty);
        }
    }
}