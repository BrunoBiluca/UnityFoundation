using UnityEngine;

namespace UnityFoundation.Character3D
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody rb;
        private CapsuleCollider capsuleCollider;
        private Settings config;

        public Projectile Setup(Settings config)
        {
            this.config = config;
            return this;
        }

        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.isTrigger = true;
        }

        public void Update()
        {
            rb.velocity = transform.forward * config.Speed;
        }

        public void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }

        public class Settings
        {
            public float Speed;
        }
    }
}