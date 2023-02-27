using System;

namespace UnityFoundation.HealthSystem
{
    public interface IHealthBar
    {

        void Setup(float baseHealth);

        void SetCurrentHealth(float currentHealth);

        void Hide() { }

    }
}