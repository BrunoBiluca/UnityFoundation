using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.Zombies
{
    public interface IAIBrain
    {
        bool DebugMode { get; }
        bool IsWalking { get; }
        bool IsRunning { get; }
        bool IsAttacking { get; }
        bool IsWandering { get; }
        bool IsChasing { get; }
        Optional<Vector3> TargetPosition { get; }
        Optional<Transform> Target { get; }

        void SetPlayer(GameObject player);
        void Update();
        void Enabled();
        void Disabled();
    }
}