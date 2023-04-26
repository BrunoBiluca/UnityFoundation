using UnityEngine;
using System;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.CarSystem
{
    public class Checkpoint : MonoBehaviour
    {
        public event Action OnCheckpointPassed;

        private Timer canTriggerTimer;
        private BoxCollider boxCollider;


        private void Awake()
        {
            canTriggerTimer = (Timer)new Timer(2f).Start();
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