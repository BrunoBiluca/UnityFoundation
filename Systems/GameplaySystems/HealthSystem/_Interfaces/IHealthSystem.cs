using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.HealthSystem
{
    public interface IHealthSystem : IHasHealth, IDamageable, IHealable
    {
    }
}