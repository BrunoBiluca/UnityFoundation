using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface INavegationAgent
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