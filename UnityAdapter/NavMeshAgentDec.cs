using UnityEngine;
using UnityEngine.AI;

namespace Assets.UnityFoundation.UnityAdapter
{
    public class NavMeshAgentDec : INavMeshAgent
    {
        private readonly NavMeshAgent agent;

        public NavMeshAgentDec(NavMeshAgent agent)
        {
            this.agent = agent;
        }

        public float Speed { get => agent.speed; set => agent.speed = value; }

        public void Disabled() {
            agent.ResetPath(); 
            agent.enabled = false;
        }

        public bool SetDestination(Vector3 target) => agent.SetDestination(target);

        public void Update() { }
    }
}