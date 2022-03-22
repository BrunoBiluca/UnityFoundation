using UnityEngine;

namespace Assets.UnityFoundation.UnityAdapter
{
    public interface INavMeshAgent
    {
        float Speed { get; set; }
        float StoppingDistance { get; set; }
        float RemainingDistance { get; }
        bool SetDestination(Vector3 target);
        void ResetPath();
        void Update();
        void Disabled();
    }
}