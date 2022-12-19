using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public abstract class BaseProjectileFactory<TProjectile>
        : MonoBehaviour, IProjectileFactory
        where TProjectile : IProjectile, new()
    {
        [SerializeField] private GameObject projectilePrefab;
        private GameObject projObj;
        private IProjectile proj;

        public IProjectile Create(Vector3 start, Vector3 target)
        {
            projObj = Instantiate(projectilePrefab, start, Quaternion.identity);
            proj = new TProjectile();

            var config = new IProjectile.Settings() {
                Transform = new TransformDecorator(projObj.transform),
                Speed = ProjectileSpeed(),
                TargetPos = target
            };

            proj.Setup(config);
            proj.OnReachTarget += ResetProjectile;

            return proj;
        }

        public void Update()
        {
            if(proj != null)
            {
                proj.Update(Time.deltaTime);
            }
        }

        private void ResetProjectile()
        {
            proj = null;
            Destroy(projObj);
        }

        protected abstract float ProjectileSpeed();
    }
}
