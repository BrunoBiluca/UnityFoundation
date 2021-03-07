using System;
using UnityEngine;

namespace Assets.UnityFoundation.HealthSystem {
    public class HealthSystem : MonoBehaviour, IDamageable {

        [SerializeField]
        private HealthBar healthBar;

        private float baseHealth;
        public float CurrentHealth { get; private set; }
        public EventHandler OnDied;

        public void Awake() {
            healthBar = transform.Find("healthBar").GetComponent<HealthBar>();
        }

        public void Setup(float baseHealth) {
            this.baseHealth = baseHealth;
            CurrentHealth = baseHealth;

            healthBar.Setup(baseHealth);
        }


        public void Damage(float amount) {
            CurrentHealth -= amount;

            if(healthBar != null) {
                healthBar.SetSize(CurrentHealth);
            }

            if(CurrentHealth <= 0f) {
                OnDied?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }
    }
}