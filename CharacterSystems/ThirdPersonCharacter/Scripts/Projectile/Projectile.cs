using System;
using UnityEngine;
using UnityFoundation.Code;
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
        private Optional<TrailRenderer> trail = Optional<TrailRenderer>.None();

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

            var trailRenderer = GetComponent<TrailRenderer>();
            if(trailRenderer != null)
                trail = Optional<TrailRenderer>.Some(trailRenderer);

            if(settings != null)
                config = settings.Config;
        }

        protected override void OnActivate()
        {
            trail.Some(t => {
                t.emitting = true;
                t.Clear();
            });
        }

        public void Update()
        {
            rb.velocity = transform.forward * config.Speed;
        }

        public void OnTriggerEnter(Collider other)
        {
            trail.Some(t => t.emitting = false);
            Destroy();
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
        }
    }
}