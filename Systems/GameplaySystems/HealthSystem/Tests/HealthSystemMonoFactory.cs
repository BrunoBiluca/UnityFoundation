using UnityEngine;

namespace UnityFoundation.HealthSystem.Tests
{
    public class HealthSystemMonoFactory : IHealthSystemFactory
    {
        public IHealthSystem Create()
        {
            var healthSystem = new GameObject("health").AddComponent<HealthSystemMono>();
            healthSystem.Awake();
            return healthSystem;
        }
    }
}