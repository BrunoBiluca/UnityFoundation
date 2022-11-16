using UnityEngine;

namespace UnityFoundation.HealthSystem.Tests
{
    public class HealthSystemMonoFactory : IHealthSystemFactory
    {
        public IHealthSystem Create()
        {
            return new GameObject("health").AddComponent<HealthSystemMono>();
        }
    }
}