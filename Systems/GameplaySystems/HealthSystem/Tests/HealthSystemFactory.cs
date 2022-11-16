namespace UnityFoundation.HealthSystem.Tests
{
    public class HealthSystemFactory : IHealthSystemFactory
    {
        public static HealthSystemFactory Instance()
        {
            return new HealthSystemFactory();
        }
        public IHealthSystem Create()
        {
            return new HealthSystem();
        }
    }
}