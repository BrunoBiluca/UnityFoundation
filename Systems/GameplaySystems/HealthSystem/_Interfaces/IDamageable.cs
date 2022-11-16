using System;

namespace UnityFoundation.HealthSystem
{
    public interface IDamageable
    {
        event Action OnTakeDamage;

        DamageableLayer Layer { get; }

        /// <summary>
        /// Give damage by the positive amount.
        /// </summary>
        /// <param name="amount">Positive float</param>
        /// <param name="layer"></param>
        void Damage(float amount, DamageableLayer layer = null);
    }
}