using System;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public interface IHasHealth
    {
        float BaseHealth { get; }
        float CurrentHealth { get; }
        bool IsDead { get; }
        bool DestroyOnDied { get; set; }

        event EventHandler OnTakeDamage;
        event EventHandler OnFullyHeal;
        event EventHandler OnDied;

        void Setup(float baseHealth);
    }
}