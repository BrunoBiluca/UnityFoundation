using System;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public interface IHasHealth
    {
        float BaseHealth { get; }
        float CurrentHealth { get; }

        event EventHandler OnTakeDamage;
        event EventHandler OnFullyHeal;
        event EventHandler OnDied;
    }
}