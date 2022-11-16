using System;

namespace UnityFoundation.HealthSystem
{
    public interface IHasHealth
    {
        float BaseHealth { get; }
        float CurrentHealth { get; }
        bool IsDead { get; }

        event Action OnDied;

        void Setup(float baseHealth);
    }
}