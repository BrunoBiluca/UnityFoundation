using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Zombies.Tests
{
    public class DummyNavMeshAgent : INavMeshAgent
    {
        public Transform Body { get; }
        public float Speed { get ; set ; }
        public float StoppingDistance { get; set; }

        public float RemainingDistance => 0f;

        private Vector3 destination;

        public DummyNavMeshAgent(Transform body)
        {
            Body = body;
        }

        public bool SetDestination(Vector3 target)
        {
            destination = target;
            return true;
        }

        public void Update()
        {
            Body.position += new Vector3(Speed, 0, 0);
        }

        public void Disabled()
        {
        }

        public void ResetPath()
        {
        }
    }
}