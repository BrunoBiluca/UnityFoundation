using System;
using UnityEngine;
using UnityFoundation.Code.Features;

namespace UnityFoundation.ThirdPersonCharacter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Projectile : PooledObject
    {
        private Rigidbody rb;
        private CapsuleCollider capsuleCollider;
        private Settings config;

        [SerializeField] private ProjectileSettingsSO settings;
        private TrailRenderer trail;

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

            trail = GetComponent<TrailRenderer>();

            if(settings != null)
                config = settings.Config;
        }

        protected override void OnActivate()
        {
            trail.emitting = true;
            trail.Clear();
        }

        public void Update()
        {
            rb.velocity = transform.forward * config.Speed;
        }

        public void OnTriggerEnter(Collider other)
        {
            trail.emitting = false;
            Destroy();
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
        }
    }
}