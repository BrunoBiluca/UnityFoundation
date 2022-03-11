using UnityEngine;
using UnityFoundation.Code.TimeUtils;
using System;

namespace Assets.UnityFoundation.Systems.CarSystem
{
    public class Checkpoint : MonoBehaviour
    {
        public event Action OnCheckpointPassed;

        private Timer canTriggerTimer;
        private BoxCollider boxCollider;


        private void Awake()
        {
            canTriggerTimer = new Timer(2f).Start();
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!canTriggerTimer.Completed) return;

            canTriggerTimer.Start();

            if(other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;

            var direction = other.transform.position - (transform.position + boxCollider.center);

            if(Vector3.Dot(transform.forward, direction) < 0)
            {
                OnCheckpointPassed?.Invoke();
            }
        }
    }
}