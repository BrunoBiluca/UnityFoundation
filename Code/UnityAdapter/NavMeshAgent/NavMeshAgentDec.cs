using UnityEngine;
using UnityEngine.AI;

namespace UnityFoundation.Code.UnityAdapter
{
    public class NavMeshAgentDec : INavMeshAgent
    {
        private readonly NavMeshAgent agent;

        public NavMeshAgentDec(NavMeshAgent agent)
        {
            this.agent = agent;
        }

        public float Speed { get => agent.speed; set => agent.speed = value; }

        public float StoppingDistance {
            get => agent.stoppingDistance;
            set => agent.stoppingDistance = value;
        }

        public float RemainingDistance => agent.remainingDistance;

        public void Disabled() {
            agent.ResetPath();
            agent.enabled = false;
        }

        public void ResetPath() {
            if(agent.isActiveAndEnabled)
                agent.ResetPath();
        }

        public bool SetDestination(Vector3 target) => agent.SetDestination(target);

        public void Update() { }
    }
}