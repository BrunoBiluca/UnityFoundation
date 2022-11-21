using UnityFoundation.Code.UnityAdapter;
using System;
using UnityEngine;

namespace UnityFoundation.HealthSystem
{
    public class HealthSystemMono : BilucaMono, IHealthSystem
    {
        [SerializeField] private bool setupOnStart = false;

        // TODO: criar aqui uma annotation de restrição
        // para elementos que possui um component IHealthBar
        [SerializeField]
        private GameObject healthBarComponent;
        private IHealthBar healthBar;

        [SerializeField] private float baseHealth;

        [SerializeField] private float currentHealth;
        [field: SerializeField] public bool DestroyHealthBarOnDied { get; set; }

        public float BaseHealth => healthSystem.BaseHealth;
        public float CurrentHealth => healthSystem.CurrentHealth;
        public bool IsDead => healthSystem.IsDead;

        [SerializeField] private DamageableLayer layer;
        public DamageableLayer Layer {
            get { return layer; }
            set { layer = value; }
        }

        [field: SerializeField] public bool DestroyOnDied { get; set; } = false;


        public event Action OnFullyHeal;
        public event Action OnDied;
        public event Action OnTakeDamage;

        private DamageableLayerManager damageableLayerManager;
        private Func<bool> guardDamageCallback;

        private IHealthSystem healthSystem;

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

            healthBar ??= gameObject.GetComponentInChildren<IHealthBar>();
        }

        public void SetHealthBar(IHealthBar bar)
        {
            healthBar = bar;
        }

        public void SetDamageableLayerManager(DamageableLayerManager manager)
        {
            damageableLayerManager = manager;
        }

        public void Setup(float baseHealth)
        {
            healthSystem = new HealthSystem();
            healthSystem.OnFullyHeal += FullyHealHandler;
            healthSystem.OnTakeDamage += TakeDamageHandler;
            healthSystem.OnDied += DieHandler;

            healthSystem.Setup(baseHealth);

            HealthBarReference();
            healthBar?.Setup(baseHealth);
        }

        private void FullyHealHandler()
        {
            OnFullyHeal?.Invoke();
        }

        private void TakeDamageHandler()
        {
            OnTakeDamage?.Invoke();
        }

        private void DieHandler()
        {
            OnDied?.Invoke();

            if(DestroyHealthBarOnDied) Destroy(healthBarComponent);
            if(DestroyOnDied) Destroy();
        }

        public void SetGuardDamage(Func<bool> callback)
        {
            guardDamageCallback = callback;
        }

        public void Damage(float amount, DamageableLayer layer = null)
        {
            if(!CanInflictDamage(layer))
                return;

            healthSystem.Damage(amount, layer);

            if(healthBarComponent != null)
                healthBar?.SetCurrentHealth(CurrentHealth);
        }

        private bool CanInflictDamage(DamageableLayer layer)
        {
            if(IsDead)
                return false;

            if(
                damageableLayerManager != null
                && !damageableLayerManager.LayerCanDamage(layer, Layer)
            )
                return false;

            if(guardDamageCallback != null && guardDamageCallback())
                return false;

            return true;
        }

        public void Heal(float amount)
        {
            healthSystem.Heal(amount);
            healthBar?.SetCurrentHealth(CurrentHealth);
        }

        public void HealFull()
        {
            healthSystem.HealFull();
            healthBar?.SetCurrentHealth(CurrentHealth);
        }
    }
}