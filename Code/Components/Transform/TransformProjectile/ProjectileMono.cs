using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public class ProjectileMono : MonoBehaviour, IDestroyable
    {
        private Transform trail;
        private Transform impactEffect;

        public event Action OnObjectDestroyed;

        public ITransform Transform { get; private set; }

        public void Awake()
        {
            trail = transform.Find("trail");
            impactEffect = transform.Find("impact_effect");

            Transform = new TransformDecorator(transform);
        }

        public void Destroy()
        {
            trail.parent = null;

            impactEffect.parent = null;
            impactEffect.GetComponent<IVisible>().Show();
            Destroy(gameObject);
        }
    }
}
