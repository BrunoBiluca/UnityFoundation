using Assets.UnityFoundation.Code.MonoBehaviourUtils;
using System;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public class HealthSystem : CustomDestroyMonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private bool setupOnStart = false;

        // TODO: criar aqui uma annotation de restrição
        // para elementos que possui um component IHealthBar
        [SerializeField]
        private GameObject healthBarComponent;
        private IHealthBar healthBar;

        [SerializeField] private float baseHealth;
        public float BaseHealth { get { return baseHealth; } }

        [SerializeField] private bool destroyHealthBarOnDied = false;

        [SerializeField] private bool destroyOnDied = true;
        
        [SerializeField] private float currentHealth;
        public float CurrentHealth {
            get { return currentHealth; }
            private set { currentHealth = value; }
        }

        [SerializeField] private DamageableLayer layer;
        public DamageableLayer Layer {
            get { return layer; }
            set { layer = value; }
        }

        public event EventHandler OnTakeDamage;
        public event EventHandler OnFullyHeal;
        public event EventHandler OnDied;

        private DamageableLayerManager damageableLayerManager;
        private Func<bool> guardDamageCallback;

        public void Start()
        {
            if(setupOnStart)
                Setup(baseHealth);
        }

        private void HealthBarReference()
        {
            if(healthBar == null && healthBarComponent != null)
            {
                healthBar = healthBarComponent.GetComponent<IHealthBar>();
                return;
            }

            if(healthBar == null)
            {
                healthBar = gameObject.GetComponentInChildren<IHealthBar>();
            }
        }

        public void Setup(float baseHealth)
        {
            this.baseHealth = baseHealth;
            CurrentHealth = baseHealth;

            HealthBarReference();
            healthBar?.Setup(baseHealth);

            damageableLayerManager = DamageableLayerManager.Instance;

            OnFullyHeal?.Invoke(this, EventArgs.Empty);
        }

        public void SetDestroyOnDied(bool value) => destroyOnDied = value;

        public void SetDestroyHealthbar(bool value) => destroyHealthBarOnDied = value;


        public void SetGuardDamage(Func<bool> callback)
        {
            guardDamageCallback = callback;
        }

        public void Damage(float amount, DamageableLayer layer = null)
        {
            if(!CanInflictDamage(layer))
                return;

            CurrentHealth -= Mathf.Abs(EvaluateDamage(amount));

            healthBar?.SetCurrentHealth(CurrentHealth);

            if(CurrentHealth <= 0f)
            {
                OnDied?.Invoke(this, EventArgs.Empty);

                if(destroyHealthBarOnDied) Destroy(healthBarComponent);
                if(destroyOnDied) destroyBehaviour.Destroy();
                return;
            }

            if(CurrentHealth < baseHealth)
            {
                OnTakeDamage?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool CanInflictDamage(DamageableLayer layer)
        {
            if(
                damageableLayerManager != null
                && !damageableLayerManager.HasRelationship(layer, Layer)
            )
                return false;

            if(guardDamageCallback != null && guardDamageCallback())
                return false;

            return true;
        }

        protected virtual float EvaluateDamage(float amount) => amount;

        public void Heal(float amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, baseHealth);

            healthBar?.SetCurrentHealth(CurrentHealth);

            if(CurrentHealth == baseHealth)
            {
                OnFullyHeal?.Invoke(this, EventArgs.Empty);
            }
        }

        public void HealFull()
        {
            CurrentHealth = baseHealth;

            healthBar?.SetCurrentHealth(CurrentHealth);

            OnFullyHeal?.Invoke(this, EventArgs.Empty);
        }
    }
}