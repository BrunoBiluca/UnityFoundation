namespace UnityFoundation.Code
{

    public class ProjectileFactory : BaseProjectileFactory<TransformProjectile>
    {
        protected override float ProjectileSpeed() => 20;
    }
}
