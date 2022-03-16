using UnityEngine;

namespace Assets.UnityFoundation.UnityAdapter
{
    public interface INavMeshAgent
    {
        float Speed { get; set; }
        bool SetDestination(Vector3 target);
        void Update();
        void Disabled();
    }
}