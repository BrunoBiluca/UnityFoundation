using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public class ProjectileFactory<TProjectile>
        : MonoBehaviour, IProjectileFactory
        where TProjectile : IProjectile, new()
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private float projectileSpeed;

        private GameObject projObj;
        private IProjectile proj;

        public IProjectile Create(Vector3 start, Vector3 target)
        {
            projObj = Instantiate(projectilePrefab, start, Quaternion.identity);
            proj = new TProjectile();

            var config = new IProjectile.Settings() {
                Transform = new TransformDecorator(projObj.transform),
                Speed = projectileSpeed,
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
            if(explosionPrefab != null)
                Instantiate(explosionPrefab, projObj.transform.position, Quaternion.identity);

            Destroy(projObj);
        }
    }
}
