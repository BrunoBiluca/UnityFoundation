namespace Assets.UnityFoundation.Systems.HealthSystem
{

    public interface IDamageable {

        public void Damage(float amount, DamageableLayer layer = null);
    }
}