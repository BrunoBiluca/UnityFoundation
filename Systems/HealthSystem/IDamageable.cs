namespace Assets.UnityFoundation.Systems.HealthSystem
{

    public interface IDamageable {

        public DamageableLayer Layer { get; }

        public void Damage(float amount, DamageableLayer layer = null);
    }
}