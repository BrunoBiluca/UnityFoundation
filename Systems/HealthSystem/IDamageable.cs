namespace Assets.UnityFoundation.HealthSystem
{

    public interface IDamageable {

        public void Damage(float amount, DamageableLayer layer = null);
    }
}