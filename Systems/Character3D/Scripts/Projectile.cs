using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.Character3D
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            rb.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}