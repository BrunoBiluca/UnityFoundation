using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityFoundation.Code.UnityAdapter
{
    public class NavMeshAgentDec : INavegationAgent
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

        public event Action OnReachDestination;

        public void Disabled()
        {
            agent.ResetPath();
            agent.enabled = false;
        }

        public void ResetPath()
        {
            if(agent.isActiveAndEnabled)
                agent.ResetPath();
        }

        public bool SetDestination(Vector3 target) => agent.SetDestination(target);

        public void Update(float updateTime = 1) { }
    }
}