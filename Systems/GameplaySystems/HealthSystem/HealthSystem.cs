using System;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.HealthSystem
{
    public class HealthSystem : IHealthSystem
    {
        public float BaseHealth { get; private set; }

        public float CurrentHealth { get; private set; }

        private ValueEvaluation<float> currentHealthEval;

        public bool IsDead { get; private set; }

        public DamageableLayer Layer { get; private set; }

        public event Action OnFullyHeal;
        public event Action OnDied;
        public event Action OnTakeDamage;

        public void Damage(float amount, DamageableLayer layer = null)
        {
            UpdateHealth(-Mathf.Abs(EvaluateDamage(amount)));

            if(!IsDead)
                OnTakeDamage?.Invoke();
        }

        public void Heal(float amount)
        {
            UpdateHealth(Mathf.Abs(amount));
        }

        public void HealFull()
        {
            UpdateHealth(BaseHealth);
        }

        public void Setup(float baseHealth)
        {
            BaseHealth = baseHealth;
            CurrentHealth = baseHealth;

            currentHealthEval = ValueEvaluation<float>.Create(() => CurrentHealth);
            currentHealthEval
                .If((health) => health <= 0f)
                .Do(() => {
                    IsDead = true;
                    OnDied?.Invoke();
                });

            currentHealthEval
                .If((health) => CurrentHealth == BaseHealth)
                .Do(() => {
                    OnFullyHeal?.Invoke();
                });
        }

        protected virtual float EvaluateDamage(float amount)
        {
            return amount;
        }

        private void UpdateHealth(float amount)
        {
            if(IsDead) return;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, BaseHealth);

            currentHealthEval.Eval();
        }

    }
}