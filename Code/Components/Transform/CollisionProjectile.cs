using UnityFoundation.Code.UnityAdapter;
using System;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class CollisionProjectile : BilucaMono
    {
        public event Action<IGameObject> OnHit;

        [SerializeField] private float projectileDamage = 1f;
        [SerializeField] private float projectileSpeed;

        private Vector3 direction;

        protected override void OnAwake()
        {
            if(projectileSpeed == 0)
                Debug.LogWarning("Projectile speed was not configure");
        }

        public void OnEnable()
        {
            Obj.DestroyBehaviour.Destroy(1f);
        }

        public void Setup(int direction)
        {
            this.direction = new Vector3(direction, 0, 0);
        }

        void Update()
        {
            transform.position += projectileSpeed * Time.deltaTime * direction;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Obj.DestroyBehaviour.Destroy();

            OnHit?.Invoke(new GameObjectDecorator(collision.gameObject));
        }
    }
}