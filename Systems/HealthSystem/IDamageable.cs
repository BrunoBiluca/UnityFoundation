namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public interface IDamageable : IHasHealth
    {
        public DamageableLayer Layer { get; }

        public void Damage(float amount, DamageableLayer layer = null);
    }
}