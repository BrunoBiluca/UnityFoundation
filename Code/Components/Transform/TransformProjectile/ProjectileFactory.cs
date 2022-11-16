using UnityEngine;

namespace UnityFoundation.Code
{
    public class ProjectileFactory : MonoBehaviour
    {
        [SerializeField] GameObject ProjectilePrefab;

        private TransformProjectile proj;

        public TransformProjectile Create(Vector3 startPosition, Vector3 targetPosition)
        {
            var projectile = Instantiate(ProjectilePrefab, startPosition, Quaternion.identity)
                .GetComponent<ProjectileMono>();
            //projectile.Awake();

            var projectileSettings = new TransformProjectile.Settings() {
                Projectile = projectile,
                Speed = 20,
                TargetPosition = targetPosition + Vector3.up,
            };

            proj = new TransformProjectile(projectileSettings);
            proj.OnReachTarget += ResetProjectile;

            return proj;
        }

        private void ResetProjectile()
        {
            proj = null;
        }

        public void Update()
        {
            if(proj != null)
            {
                proj.Update(Time.deltaTime);
            }
        }
    }
}
