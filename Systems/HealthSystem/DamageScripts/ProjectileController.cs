using Assets.UnityFoundation.Code.MonoBehaviourUtils;
using System;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem.DamageScripts
{
    public class ProjectileController : CustomDestroyMonoBehaviour
    {
        public event EventHandler OnShootDestroy;

        [SerializeField] private float projectileDamage = 1f;
        [SerializeField] private float projectileSpeed;

        private Vector3 direction;
        private GameObject player;

        protected override void OnAwake()
        {
            destroyBehaviour.OnBeforeDestroy(
                () => OnShootDestroy?.Invoke(this, EventArgs.Empty)
            );

            if(projectileSpeed == 0)
                Debug.LogWarning("Projectile speed was not configure");
        }

        public void OnEnable()
        {
            destroyBehaviour.Destroy(1f);
        }

        public void Setup(int direction, GameObject player)
        {
            this.direction = new Vector3(direction, 0, 0);
            this.player = player;
        }

        void Update()
        {
            transform.position += direction * projectileSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            destroyBehaviour.Destroy();

            if(!collision.gameObject.TryGetComponent(out IDamageable entity))
                return;

            entity.Damage(projectileDamage, player.GetComponent<IDamageable>().Layer);
        }
    }
}