using System;

namespace UnityFoundation.HealthSystem
{
    public interface IHealable
    {
        event Action OnFullyHeal;

        void Heal(float amount);
        void HealFull();
    }
}