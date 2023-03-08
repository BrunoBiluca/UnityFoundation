using System;
using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody rb;
        private CapsuleCollider capsuleCollider;
        private Settings config;

        [SerializeField] private ProjectileSettingsSO settings;

        public Projectile Setup(Settings config)
        {
            this.config = config;
            return this;
        }

        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.mass = 0f;
            rb.useGravity = false;
            capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.isTrigger = true;

            if(settings != null)
                config = settings.Config;
        }

        public void Update()
        {
            rb.velocity = transform.forward * config.Speed;
        }

        public void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
        }
    }
}